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

API void* MSP_Double_New(void* game) { return new MixedStrategyProfile<double>(grep(game)->NewMixedStrategyProfile(0.0)); }

API double MSP_Double_GetStrategyProbability(void* profile, void* s) { return mspdrep(profile)->operator[](gsrep(s)); }

API void* MSP_Double_SetStrategyProbability(void* profile, void* s, double p) { mspdrep(profile)->operator[](gsrep(s)) = p; return profile; }

API double MSP_Double_GetPlayerPayoff(void* profile, void* player) { return mspdrep(profile)->GetPayoff(gprep(player)); }

API double MSP_Double_GetStrategyPayoff(void* profile, void* strategy) { return mspdrep(profile)->GetPayoff(gsrep(strategy)); }

API double MSP_Double_GetPlayerNumPayoff(void* profile, int player) { return mspdrep(profile)->GetPayoff(player); }

API void* MSP_Rational_New(void* game) { return new MixedStrategyProfile<Rational>(grep(game)->NewMixedStrategyProfile(Rational(1, 1))); }

API double MSP_Rational_GetStrategyProbability(void* profile, void* s) { return msprrep(profile)->operator[](gsrep(s)); }

API void MSP_Rational_GetStrategyProbabilityRational(void* profile, void* s, CS_OUT long& num, CS_OUT long& den)
{ 
	NumDen(Number(msprrep(profile)->operator[](gsrep(s))), num, den);
}

API void* MSP_Rational_SetStrategyProbability(void* profile, void* s, double p) { msprrep(profile)->operator[](gsrep(s)) = Rational(p); return profile; }

API double MSP_Rational_GetPlayerPayoff(void* profile, void* player) { return msprrep(profile)->GetPayoff(gprep(player)); }

API double MSP_Rational_GetStrategyPayoff(void* profile, void* strategy) { return msprrep(profile)->GetPayoff(gsrep(strategy)); }

API double MSP_Rational_GetPlayerNumPayoff(void* profile, int player) { return msprrep(profile)->GetPayoff(player); }