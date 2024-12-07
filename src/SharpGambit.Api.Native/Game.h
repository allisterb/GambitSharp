#pragma once
#include "pch.h"
#include "api.h"

using namespace Gambit;

API void* NewStrategicFormGame(const char* title, int pc, const char* players[], const int strategies[])
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

API void* AddPlayer(void* game) { return gpptr(grep(game)->NewPlayer()); }
API void* GetPlayer(void* game, int pi) { return gpptr(grep(game)->GetPlayer(pi)); }

	


