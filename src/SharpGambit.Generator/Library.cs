namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Parser;

public class Library : Runtime, ILibrary
{    
    #region Constructors
    public Library(Dictionary<string, object> options)
    {
        BindOptions = options;
        foreach (System.Reflection.PropertyInfo prop in this.GetType().GetProperties())
        {
            if (BindOptions.ContainsKey(prop.Name) &&  BindOptions[prop.Name].GetType() == prop.PropertyType && prop.CanWrite)
            {
                prop.SetValue(this, BindOptions[prop.Name]);
            }
        }
        Contract.Requires(RootDirectory.Exists);
        if (string.IsNullOrEmpty(OutputDirName))
        {
            OutputDirName = Directory.GetCurrentDirectory();
        }
        if (string.IsNullOrEmpty(Class))
        {
            Class = ModuleName;
        }
        if (string.IsNullOrEmpty(Namespace))
        {
            Namespace = Name;
        }
            
        Contract.Requires(!string.IsNullOrEmpty(ModuleName));
        Contract.Requires(!string.IsNullOrEmpty(OutputDirName));
        Contract.Requires(!string.IsNullOrEmpty(Class));
        Contract.Requires(!string.IsNullOrEmpty(Namespace));
        F = Path.Combine(Path.GetFullPath(OutputDirName), ModuleName + ".cs");

        Info($"Binding library module {ModuleName}.");
        Info($"Using {R} as library directory.");
        Info($"Using {Path.GetFullPath(OutputDirName)} as output directory.");
        Info($"Using {Namespace} as library namespace.");
            
        if (File.Exists(F))
        {
            Warn($"Module file {F} will be overwritten.");
        }
        else
        {
            Info($"Module file is {F}.");
        }
    }
    #endregion

    #region Implemented members
    /// Setup the driver options here.
    public void Setup(Driver driver)
    {
        DriverOptions options = driver.Options;
        options.GeneratorKind = GeneratorKind.CSharp;
        options.Verbose = true;
        driver.ParserOptions.TargetTriple = "x86_64-pc-win32-msvc";
        options.MarshalCharAsManagedChar = true;
        Module = options.AddModule(ModuleName);
        Module.OutputNamespace = Namespace;
        options.OutputDir = OutputDirName;
        options.GenerationOutputMode = GenerationOutputMode.FilePerUnit;
        Module.IncludeDirs.Add(Path.Combine(R, "src"));
        Module.LibraryDirs.Add(Path.Combine(R, "build", "Release"));
        //Module.Headers.Add("gambit.h");
        Module.IncludeDirs.Add(Path.Combine(R, "..", "..", "src", "SharpGambit.Native.Api"));
        Module.Headers.Add("sharpgambit.h");
        Module.LibraryDirs.Add(Path.Combine(R, "..", "..", "src", "SharpGambit.Native.Api", "bin", "x64", "Debug"));
        driver.ParserOptions.AddArguments("-fcxx-exceptions");
        driver.ParserOptions.LanguageVersion = LanguageVersion.CPP17;
        Module.SharedLibraryName = "sharpgambit";
        options.GenerateClassTemplates = true;  
        options.GenerateObjectOverrides = true;
        options.GenerateExternalDataFields = true;
        driver.ParserOptions.SkipPrivateDeclarations = false;
        options.CheckSymbols = true;
        options.CompileCode = true; 
    }

    /// Setup your passes here.
    public virtual void SetupPasses(Driver driver)
    {
        //driver.AddTranslationUnitPass(new GetAllClassDeclsPass(this, driver.Generator));
        //driver.AddTranslationUnitPass(new ConvertFunctionParameterDeclsPass(this, driver.Generator));
    }

    /// Do transformations that should happen before passes are processed.
    public void Preprocess(Driver driver, ASTContext ctx)
    {
        var arrptr = ctx.FindClass("Array").Single();
        foreach (var f in arrptr.Fields)
        {
            if (f.Name == "data")
            {
                f.Access = AccessSpecifier.Public;
                //f.QualifiedType = new QualifiedType(new PointerType(new QualifiedType(f.Type, new TypeQualifiers())));
            }
        }

        var goptr = ctx.FindClass("GameObjectPtr").Single();
        foreach (var f in goptr.Fields)
        {
            if (f.Name == "rep")
            {
                f.Access = AccessSpecifier.Public;
            }
        }

    }

    /// Do transformations that should happen after passes are processed.
    public void Postprocess(Driver driver, ASTContext ctx)
    {
       
    }
    #endregion

    #region Properties
    public string Name = "gambit";
    public Dictionary<string, object> BindOptions { get; internal set; }
    public DirectoryInfo RootDirectory { get; internal set; } = new DirectoryInfo(AssemblyLocation);
    public string R => RootDirectory.FullName;
    public string F { get; protected set; }
    public string OutputDirName { get; internal set; } = "";
    public string OutputFileName { get; internal set; } = "gambit.cs";
    public string ModuleName { get; internal set; } = "gambit";
    public Module Module { get; internal set; } = new Module("gambit");
    public string Class { get; internal set; } = "";
    public string Namespace { get; internal set; } = "";
    public bool WithoutCommon { get; protected set; }
    public bool Verbose { get; internal set; }
    #endregion

    #region Methods
   
    #endregion

    #region Fields
    public List<string> ClassDecls = new List<string>();
    #endregion
}

