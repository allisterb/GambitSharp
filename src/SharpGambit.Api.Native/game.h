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

API void* SetTitle(const char* title, void* game) { auto g = grep(game); g->SetTitle(title); return g; }

API const char* GetTitle(void* game) { return grep(game)->GetTitle().c_str(); }

API void* NewPlayer(void* game) { return gpptr(grep(game)->NewPlayer()); }

API void* GetPlayer(void* game, int pi) { return gpptr(grep(game)->GetPlayer(pi)); }

API const intptr_t* GetPlayers(void* game, CS_OUT int& size) 
{ 
	auto players = grep(game)->GetPlayers();
	return ToCPtrArray(players, size);
}

API int NumPlayers(void* game) { return grep(game)->NumPlayers(); }

API const int* StrategyCounts(void* game) 
{ 
	auto arr =  gerep(game)->NumStrategies();
	int* a = new int[arr.Length()];
	for (int i = 0; i < arr.Length(); i++)
	{
		a[i] = arr[i + 1];
	}
	return a;
}

API void* NewOutcome(void* game) { return goptr(gerep(game)->NewOutcome()); }

API void* GetOutcome(void* game, int index) { return goptr(gerep(game)->GetOutcome(index)); }

API void* NewTablePureStrategyProfile(void* game) { return new PureStrategyProfile(gtablerep(game)->NewPureStrategyProfile()); }

API const char* GetLatex(void* game)
{
	auto w = new LaTeXGameWriter();
	return (new std::string(w->Write(grep(game)).c_str()))->c_str();
}
	


