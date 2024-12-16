﻿using System;

namespace SharpGambit;

public static unsafe class Utils
{
    
    public static nint[] GetPointerAray(IntPtr _ptr, int size)
    {
        var ptr = (long*)_ptr;
        var arr = new nint[size]; 
        for (int i = 0;  i < size; i++)
        {
            arr[i] = (nint)ptr[i];
        }
        return arr;
    }
    
}
