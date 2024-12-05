#pragma once

#include "pch.h"

#include "Collections.h"
#include "Game.h"

using namespace Gambit;

static void* gptr(Game game) { return (GameRep*)game; }
static Game grep(void * game) { return reinterpret_cast<GameRep*>(game); }

static void* gpptr(GamePlayer player) { return (GamePlayerRep*) (player); }
static GamePlayer gprep(void* player) { return reinterpret_cast<GamePlayerRep*>(player); }

static void* gsptr(GameStrategy strategy) { return (GameStrategyRep*) (strategy); }
static GameStrategy gsrep(void* strategy) { return reinterpret_cast<GameStrategyRep*>(strategy); }

API void* NewEmptyGame() { auto g = NewTree(); g->IncRef();  return gptr(g); }
API void* NewGame(const char* title, int pc = 0, const char* players[] = nullptr)
{
	auto g = Gambit::NewTree();
	g->IncRef();
	g->SetTitle(title);
	for (int i = 0; i < pc; i++)
	{
		auto p = g->NewPlayer();
		p->SetLabel(players[i]);
	}
	return gptr(g);
}

API void* AddPlayerToGame(void* game) { return gpptr(grep(game)->NewPlayer()); }
API void* GetPlayer(void* game, int pi) { return gpptr(grep(game)->GetPlayer(pi)); }
API const char* GetPlayerLabel(void* player) { return gprep(player)->GetLabel().c_str(); }
API void* SetPlayerLabel(void* player, const char* label) { gprep(player)->SetLabel(label); return player; }

API int GetPlayerNumber(void* player) { return gprep(player)->GetNumber(); }

API void* NewPlayerStrategy(void* player) { return gsptr(gprep(player)->NewStrategy()); }
API void* GetStrategy(void* player, int st) { return gsptr(gprep(player)->GetStrategy(st)); }
API void* GetStrategy(void* player, int st) { return gsptr(gprep(player)->Get); }
