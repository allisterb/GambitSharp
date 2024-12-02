#pragma once

#include "pch.h"

#include "Collections.h"

API Gambit::Game NewGame() { return Gambit::NewTree();}
API Gambit::Game NewGame(int s[]) { return Gambit::NewTable(ArrayFromCArray<int>(s)); }

API Gambit::GamePlayer AddPlayerToGame(Gambit::Game& game) { return game->NewPlayer(); }
API const char* GetPlayerTitle(Gambit::GamePlayer& player) { return player->GetLabel().c_str(); }
API Gambit::GamePlayer SetPlayerTitle(Gambit::GamePlayer& player, const char* label) { player->SetLabel(label); return player; }

