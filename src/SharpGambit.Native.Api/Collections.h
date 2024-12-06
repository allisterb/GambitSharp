#pragma once
#include "pch.h"

using namespace Gambit;

template <class T>
Gambit::Array<T> FromCArray(int arrc, const T arr[])
{
	Gambit::Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i] = arr[i];
	}
	return a;
}

template <class T>
void * ToCArray(const Array<GameObjectPtr<T>> arr)
{
	return (T*) arr[0];
}

