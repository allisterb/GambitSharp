#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* PSP_New(void* game) {return new PureStrategyProfile((grep(game)->NewPureStrategyProfile())); }

API int PSP_GetIndex(void* profile) { return (*psprep(profile))->GetIndex(); }

API void* PSP_GetStrategy(void* profile, int pl) { return gsptr((*psprep(profile))->GetStrategy(pl)); }

API void* PSP_SetStrategy(void* profile, void* s) { (*psprep(profile))->SetStrategy(gsrep(s)); return profile; }

API void* PSP_GetOutcome(void* profile) { return goptr((*psprep(profile))->GetOutcome()); }