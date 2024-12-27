#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* PSP_New(void* game) {return new PureStrategyProfile((grep(game)->NewPureStrategyProfile())); }

API int PSP_GetIndex(void* profile) { return (*psprep(profile))->GetIndex(); }

API void* PSP_GetStrategy(void* profile, int pl) { return gsptr((*psprep(profile))->GetStrategy(pl)); }

API void* PSP_SetStrategy(void* profile, void* s) { (*psprep(profile))->SetStrategy(gsrep(s)); return profile; }

API void* PSP_GetOutcome(void* profile) { return goptr((*psprep(profile))->GetOutcome()); }

API void* PSP_SetOutcome(void* profile, void* o) { (*psprep(profile))->SetOutcome(gorep(o)); return profile; }

API void* MSP_New(void* game) { return new MixedStrategyProfile<double>(grep(game)->NewMixedStrategyProfile(0.0)); }

API double MSP_GetStrategyProbability(void* profile, void* s) { return msprep(profile)->operator[](gsrep(s)); }

API void* MSP_SetStrategyProbability(void* profile, void* s, double p) { msprep(profile)->operator[](gsrep(s)) = p; return profile; }