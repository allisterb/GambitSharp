#include "pch.h"
#include "Game.h"
#include "Player.h"
#include "Collections.h"
using namespace SharpGambit;

Game::Game() :
	game(Gambit::NewTree()) {}

Game::Game(int* ptr, int len)
{
	//me = Gambit::NewTable(ArrayFromPointer(ptr, len), false);
}

Player Game::NewPlayer() {
	return Player(game->NewPlayer());
}