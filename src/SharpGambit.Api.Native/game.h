#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* NewNormalFormGame(const char* title, int pc, const char* players[], const int strategies[])
{
	auto g = Gambit::NewTable(FromCArray(pc, strategies));
	g->IncRef();
	g->SetTitle(title);
	for (int i = 0; i < pc; i++)
	{
		auto p = g->GetPlayer(i + 1);
		p->SetLabel(players[i]);
	}
	return gptr(g);
}

API void* NewExtensiveFormGame() { auto g = Gambit::NewTree(); g->IncRef();  return gptr(g); }

API void* NewPlayer(void* game) { return gpptr(grep(game)->NewPlayer()); }
API void* GetPlayer(void* game, int pi) { return gpptr(grep(game)->GetPlayer(pi)); }

API void* NewOutcome(void* game) { return goptr(gerep(game)->NewOutcome()); }
API void* GetOutcome(void* game, int index) { return goptr(gerep(game)->GetOutcome(index)); }

API void* NewPureStrategyProfile(void* game) { return gspptr(gtablerep(game)->NewPureStrategyProfile()); }


	


