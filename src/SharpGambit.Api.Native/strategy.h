#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API const char* GetPlayerStrategyLabel(void* strategy) { return gsrep(strategy)->GetLabel().c_str(); }
API void* SetPlayerStrategyLabel(void* strategy, const char* label) { gsrep(strategy)->SetLabel(label); return strategy; }