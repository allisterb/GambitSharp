namespace GambitSharp;

using System;

using static NativeUtils;

public class NativeHelpers
{
    public unsafe static nint[] EnumPureStrategySolve(nint game) =>
        GetPointerArray(gambit.solvers.EnumPureStrategySolve(game, out var size), size);

    public unsafe static nint[] EnumMixedStrategySolve(nint game) =>
        GetPointerArray(gambit.solvers.EnumMixedStrategySolve(game, out var size), size);
}

