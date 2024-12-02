#pragma once
#include "pch.h"

template <class T>
Gambit::Array<T> API ArrayFromCArray(const T arr[])
{
	int len = sizeof(arr) / sizeof(arr[0]);
	Gambit::Array<T> a = Gambit::Array<T>(len);
	for (int i = 0; i < len; i++)
	{
		a[i] = arr[i];
	}
	return a;
}


