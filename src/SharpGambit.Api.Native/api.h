#pragma once

#include "pch.h"
#include "games/gametable.h"
#include "games/gametree.h"
#include "games/stratpure.h"
#define	API extern "C" __declspec(dllexport) 

#define CS_OUT 

using namespace std;
using namespace Gambit;

static void* gptr(Game game) { return (GameRep*)game; }
static GameRep* grep(void* game) { return reinterpret_cast<GameRep*>(game); }

static GameExplicitRep* gerep(void* game) { return reinterpret_cast<GameExplicitRep*>(game); }
static GameTableRep* gtablerep(void* game) { return reinterpret_cast<GameTableRep*>(game); }
static GameTreeRep* gtreerep(void* game) { return reinterpret_cast<GameTreeRep*>(game); }

static void* gpptr(GamePlayer player) { return (GamePlayerRep*)(player); }
static GamePlayerRep* gprep(void* player) { return reinterpret_cast<GamePlayerRep*>(player); }

static void* gsptr(GameStrategy strategy) { return (GameStrategyRep*)(strategy); }
static GameStrategyRep* gsrep(void* strategy) { return reinterpret_cast<GameStrategyRep*>(strategy); }

static void* pspptr(PureStrategyProfile& profile) { return &profile; }
static PureStrategyProfile psprep(void* profile) { return *reinterpret_cast<PureStrategyProfile*>(profile); }

static void* goptr(GameOutcome outcome) { return (GameOutcomeRep*)(outcome); }
static GameOutcomeRep* gorep(void* outcome) { return reinterpret_cast<GameOutcomeRep*>(outcome); }


template <class T>
static Array<T> FromCArray(int arrc, const T arr[])
{
	Array<T> a = Gambit::Array<T>(arrc);
	for (int i = 0; i < arrc; i++)
	{
		a[i + 1] = arr[i];
	}
	return a;
}

template <class T>
static const T* ToCArray(const Array<GameObjectPtr<T>>& arr)
{
	return arr[1];
}


template <class T>
static const T* ToCArrayAndSize(const Array<GameObjectPtr<T>>& arr, int& size)
{
	size = arr.Length;
	return arr[1];
}

template <class T>
static const intptr_t* ToCPtrArray(const Array<GameObjectPtr<T>>& arr, int& size)
{
	const T* ptr = ToCArray(arr);
	size = arr.Length();
	intptr_t* ptrs = new intptr_t[size];
	for (int i = 0; i < size; i++)
	{
		ptrs[i] = (intptr_t) (ptr + i);
	}
	return ptrs;

}
static void NumDen(Number n, CS_OUT long& num, CS_OUT long& den) {
	auto r = (Rational)n;
	num = r.numerator().as_long();
	den = r.denominator().as_long();
}