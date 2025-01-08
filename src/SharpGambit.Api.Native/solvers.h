#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;
using namespace Nash;

API void* EnumPureStrategy_Solve(void* game, CS_OUT int& size) 
{
	auto s = EnumPureStrategySolve(grep(game));
	size = s.size();
	intptr_t* ptrs = new intptr_t[size];
	for (int i = 0; i < size; i++)
	{
		ptrs[i] = (intptr_t)(new MixedStrategyProfile<Rational>(s[i + 1]));
	}
	return ptrs;
}