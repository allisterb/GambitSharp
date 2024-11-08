namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CommandLine;
using CommandLine.Text;
using CppSharp;
using Spectre.Console;

using Con = Spectre.Console.AnsiConsole;

public class Program : Runtime
{
    #region Methods

    #region Entry point
    static void Main(string[] args)
    {
        Initialize("SharpGambit", "CLI", (args.Contains("--debug") || args.Contains("-d")), true, true);
        PrintLogo();
        var result = new Parser().ParseArguments(args, optionTypes);
        result
            .WithParsed<GenerateOptions>(Generate)
            .WithNotParsed(errors => Help(result, errors));
    }
    #endregion

    static void Help(ParserResult<object> result, IEnumerable<Error> errors)
    {
        HelpText help = GetAutoBuiltHelpText(result);
        help.Heading = new HeadingInfo("SharpGambit generator command-line help");
        help.Copyright = "";
        if (errors.Any(e => e.Tag == ErrorType.VersionRequestedError))
        {
            help.Heading = new HeadingInfo("SharpGambit", AssemblyVersion.ToString(3));
            help.Copyright = "";
            Info(help);
            Exit(ExitResult.SUCCESS);
        }
        else if (errors.Any(e => e.Tag == ErrorType.HelpVerbRequestedError))
        {
            HelpVerbRequestedError error = (HelpVerbRequestedError)errors.First(e => e.Tag == ErrorType.HelpVerbRequestedError);
            if (error.Type != null)
            {
                help.AddVerbs(error.Type);
            }
            else
            {
                help.AddVerbs(optionTypes);
            }
            Info(help.ToString().Replace("--", ""));
            Exit(ExitResult.SUCCESS);
        }
        else if (errors.Any(e => e.Tag == ErrorType.HelpRequestedError))
        {
            HelpRequestedError error = (HelpRequestedError)errors.First(e => e.Tag == ErrorType.HelpRequestedError);
            help.AddVerbs(result.TypeInfo.Current);
            help.AddOptions(result);
            help.AddPreOptionsLine($"{result.TypeInfo.Current.Name.Replace("Options", "").ToLower()} options:");
            Info(help);
            Exit(ExitResult.SUCCESS);
        }
        else if (errors.Any(e => e.Tag == ErrorType.NoVerbSelectedError))
        {
            help.AddVerbs(optionTypes);
            Info(help);
            Exit(ExitResult.INVALID_OPTIONS);
        }
        else if (errors.Any(e => e.Tag == ErrorType.MissingRequiredOptionError))
        {
            MissingRequiredOptionError error = (MissingRequiredOptionError)errors.First(e => e.Tag == ErrorType.MissingRequiredOptionError);
            Info(help);
            Error("A required option is missing.");

            Exit(ExitResult.INVALID_OPTIONS);
        }
        else if (errors.Any(e => e.Tag == ErrorType.UnknownOptionError))
        {
            UnknownOptionError error = (UnknownOptionError)errors.First(e => e.Tag == ErrorType.UnknownOptionError);
            help.AddVerbs(optionTypes);
            Info(help);
            Error("Unknown option: {error}.", error.Token);
            Exit(ExitResult.INVALID_OPTIONS);
        }
        else
        {
            Error("An error occurred parsing the program options: {errors}.", errors);
            help.AddVerbs(optionTypes);
            Info(help);
            Exit(ExitResult.INVALID_OPTIONS);
        }
    }

    static void Generate(GenerateOptions go)
    {
        Dictionary<string, object> options = new Dictionary<string, object>()
        {
            {"RootDirectory", new DirectoryInfo(go.LibPath)},
        };
        Library lib = new Library(options); 
        ConsoleDriver.Run(lib);
    }

    static HelpText GetAutoBuiltHelpText(ParserResult<object> result)
    {
        return HelpText.AutoBuild(result, h =>
        {
            h.AddOptions(result);
            HelpText.DefaultParsingErrorsHandler(result, h);
            return h;
        },
        e => e);
    }

    static void PrintLogo()
    {
        Con.Write(new FigletText(font, "SharpGambit").Color(Color.Pink1));
        Con.Write(new Text($"v{AssemblyVersion.ToString(3)}\n"));
    }
    public static void Exit(ExitResult result)
    {
        if (Cts != null && !Cts.Token.CanBeCanceled)
        {
            Cts.Cancel();
            Cts.Dispose();
        }
        Environment.Exit((int)result);
    }

    public static void ExitIfFileNotFound(string filePath)
    {
        if (filePath.StartsWith("http://") || filePath.StartsWith("https://")) return;
        if (!File.Exists(filePath))
        {
            Error("The file {0} does not exist.", filePath);
            Exit(ExitResult.NOT_FOUND);
        }
    }

    public static void ExitWithSuccess() => Exit(ExitResult.SUCCESS);
    #endregion

    #region Fields
    static Type[] optionTypes =
    {
        typeof(Options), typeof(GenerateOptions)

    };
    static FigletFont font = FigletFont.Load(Path.Combine(AssemblyLocation, "chunky.flf"));
    static Dictionary<string, Type> optionTypesMap = new Dictionary<string, Type>();
    #endregion

    #region Enums
    public enum ExitResult
    {
        SUCCESS = 0,
        UNHANDLED_EXCEPTION = 1,
        INVALID_OPTIONS = 2,
        NOT_FOUND = 4,
        INVALID_INPUT = 5,
        UNKNOWN_ERROR = 7
    }
    #endregion
}
