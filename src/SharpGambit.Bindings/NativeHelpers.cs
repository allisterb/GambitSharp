namespace SharpGambit;

using System;

using static NativeUtils;

public class NativeHelpers
{
    public unsafe static nint[] EnumPureStrategySolve(nint game) =>
        GetPointerArray(gambit.solvers.EnumPureStrategySolve(game, out var size), size);
}

