#pragma once
#include "pch.h"

template <class T>
Gambit::Array<T> ArrayFromCArray(int arrc, const T arr[])
{
	Gambit::Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i] = arr[i];
	}
	return a;
}

template <class T>
void ** ArrayToCArray(const Gambit::Array<GameObjectPtr<T>>& arr, const T a[])
{
	void *a[arr->Length];
	for (int i = 0; i < arr->Length; i++)
	{
		a[i] = (T*) arr[i];
	}
	return a;
}

