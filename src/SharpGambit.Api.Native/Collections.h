#pragma once

#include "api.h"

using namespace Gambit;

template <class T>
Array<T> FromCArray(int arrc, const T arr[])
{
    Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i + 1] = arr[i ];
	}
	return a;
}

template <class T>
void * ToCArray(const Array<GameObjectPtr<T>> arr)
{
	return (T*) arr[1];
}

