using System;

namespace GambitSharp;

public static unsafe class NativeUtils
{
    public static nint[] GetPointerArray(long* ptr, int size)
    {
        var arr = new nint[size]; 
        for (int i = 0;  i < size; i++)
        {
            arr[i] = (nint)(ptr[i]);
        }
        return arr;
    }
}
