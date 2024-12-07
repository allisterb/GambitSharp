#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API const char* GetPlayerLabel(void* player) { return gprep(player)->GetLabel().c_str(); }
API void* SetPlayerLabel(void* player, const char* label) { gprep(player)->SetLabel(label); return player; }

API int GetPlayerNumber(void* player) { return gprep(player)->GetNumber(); }

API void* NewPlayerStrategy(void* player) { return gsptr(gprep(player)->NewStrategy()); }
API int GetPlayerNumStrategies(void* player) { return gprep(player)->NumStrategies(); }
API void* GetPlayerStrategy(void* player, int st) { return gsptr(gprep(player)->GetStrategy(st)); }
API void* GetPlayerStrategies(void* player) { return ToCArray(gprep(player)->GetStrategies()); }



