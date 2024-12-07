#pragma once

#include "pch.h"

#define	API extern "C" __declspec(dllexport) 

using namespace Gambit;

static void* gptr(Game game) { return (GameRep*)game; }
static Game grep(void* game) { return reinterpret_cast<GameRep*>(game); }

static void* gpptr(GamePlayer player) { return (GamePlayerRep*)(player); }
static GamePlayer gprep(void* player) { return reinterpret_cast<GamePlayerRep*>(player); }

static void* gsptr(GameStrategy strategy) { return (GameStrategyRep*)(strategy); }
static GameStrategy gsrep(void* strategy) { return reinterpret_cast<GameStrategyRep*>(strategy); }

template <class T>
Array<T> FromCArray(int arrc, const T arr[])
{
	Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i + 1] = arr[i];
	}
	return a;
}

template <class T>
void* ToCArray(const Array<GameObjectPtr<T>> arr)
{
	return (T*)arr[1];
}
