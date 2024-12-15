#pragma once

#include "pch.h"
#include "games/gametable.h"
#include "games/gametree.h"
#define	API extern "C" __declspec(dllexport) 

#define CS_OUT 

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

static void* gspptr(PureStrategyProfile profile) { return (PureStrategyProfileRep*)(profile); }
static PureStrategyProfileRep* gsprep(void* profile) { return reinterpret_cast<PureStrategyProfileRep*>(profile); }

static void* goptr(GameOutcome outcome) { return (GameOutcomeRep*)(outcome); }
static GameOutcomeRep* gorep(void* outcome) { return reinterpret_cast<GameOutcomeRep*>(outcome); }


//static void* gspptr(TablePureStrategyProfileRep profile) { return (TablePureStrategyProfileRep*)(profile); }
//static PureStrategyProfileRep* gsprep(void* profile) { return reinterpret_cast<PureStrategyProfileRep*>(profile); }

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
static T* ToCArray(const Array<GameObjectPtr<T>> arr)
{
	return (T*)arr[1];
}

static void NumDen(Number n, CS_OUT long& num, CS_OUT long& den) {
	auto r = (Rational)n;
	num = r.numerator().as_long();
	den = r.denominator().as_long();
}