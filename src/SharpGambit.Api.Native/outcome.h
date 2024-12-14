#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* SetPayoff(void* outcome, int pl, int payoff) { orep(outcome)->SetPayoff(pl, Integer(payoff)); return outcome; }
API void* SetPayoff(void* outcome, int pl, double payoff) { orep(outcome)->SetPayoff(pl, Rational(payoff)); }

API void* GetPayoff(void* outcome, int pl, CS_OUT long& num, CS_OUT long& den) { NumDen(orep(outcome)->GetPayoff(pl), num, den); }