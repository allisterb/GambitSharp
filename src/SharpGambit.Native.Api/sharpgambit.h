#pragma once

#include "pch.h"

#include "Collections.h"

API Gambit::Game NewGame() { return Gambit::NewTree();}
API Gambit::Game NewGame(int s[]) { return Gambit::NewTable(ArrayFromCArray<int>(s)); }

API Gambit::GamePlayer AddPlayerToGame(Gambit::Game game) { return game->NewPlayer(); }

