#pragma once

#include "pch.h"

#include "Collections.h"
#include "Game.h"

using namespace Gambit;

static void* gptr(Game game) { game->IncRef(); return (GameRep*)game; }
static Game grep(void * game) { return reinterpret_cast<GameRep*>(game); }

static void* gpptr(GamePlayer player) { player->IncRef();  return (GamePlayerRep*) (player); }
static GamePlayer gprep(void* player) { return reinterpret_cast<GamePlayerRep*>(player); }

API void* NewEmptyGame() { return gptr(NewTree()); }
API void* NewGame(const char* title, int pc = 0, const char* players[] = nullptr)
{
	auto g = Gambit::NewTree();
	g->SetTitle(title);
	for (int i = 0; i < pc; i++)
	{
		auto p = g->NewPlayer();
		p->SetLabel(players[i]);
	}
	return gptr(g);
}

API void* AddPlayerToGame(void* game) { return (GamePlayerRep*) (grep(game)->NewPlayer()); }
API void* GetPlayer(void* game, int pi) { return (grep(game))->GetPlayer(pi); }
API const char* GetPlayerTitle(void* player) { return gprep(player)->GetLabel().c_str(); }
API void* SetPlayerTitle(void* player, const char* label) { gprep(player)->SetLabel(label); return player; }

