#pragma once
#include "pch.h"

template <class T>
Gambit::Array<T> API ArrayFromCArray(int arrc, const T arr[])
{
	Gambit::Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i] = arr[i];
	}
	return a;
}


