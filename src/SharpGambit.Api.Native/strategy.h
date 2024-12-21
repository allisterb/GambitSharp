#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API const char* GetStrategyLabel(void* strategy) { return gsrep(strategy)->GetLabel().c_str(); }

API void* SetStrategyLabel(void* strategy, const char* label) { gsrep(strategy)->SetLabel(label); return strategy; }

API int GetStrategyIndex(void* strategy) { return gsrep(strategy)->GetNumber(); }