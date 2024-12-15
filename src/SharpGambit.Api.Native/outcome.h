#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* SetIntegerPayoff(void* outcome, int pl, int payoff) { gorep(outcome)->SetPayoff(pl, Number(Rational(payoff))); return gorep; }

API void* SetRationalPayoff(void* outcome, int pl, int num, int den) { gorep(outcome)->SetPayoff(pl, Number(Rational(num, den))); return gorep; }

API void GetPayoff(void* outcome, int pl, CS_OUT long& num, CS_OUT long& den) { NumDen(gorep(outcome)->GetPayoff(pl), num, den); }