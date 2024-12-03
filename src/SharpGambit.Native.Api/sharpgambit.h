#pragma once

#include "pch.h"

#include "Collections.h"

API Gambit::Game NewGame() { return Gambit::NewTree();}
API Gambit::Game NewGame(int sc, const int s[]) { return Gambit::NewTable(ArrayFromCArray<int>(sc, s)); }
API Gambit::Game NewGame(const char* title, int pc, const char* players[])
{
	auto g = Gambit::NewTree();
	g->SetTitle(title);
	for (int i = 0; i < pc; i++)
	{
		auto p = g->NewPlayer();
		p->SetLabel(players[i]);
	}
	return g;
}

API Gambit::GamePlayer AddPlayerToGame(Gambit::Game& game) { return game->NewPlayer(); }
API Gambit::GamePlayer GetPlayer(Gambit::Game& game, int pi) { return game->GetPlayer(pi); }
API const char* GetPlayerTitle(Gambit::GamePlayer& player) { return player->GetLabel().c_str(); }
API Gambit::GamePlayer SetPlayerTitle(Gambit::GamePlayer& player, const char* label) { player->SetLabel(label); return player; }

