#pragma once

#include "pch.h"

#include "Player.h"

namespace SharpGambit
{
	class Game
	{
	private:
		Gambit::Game game;

	public:
		API Game();
		API Game(int* ptr, int len);
		API ~Game();
		API Player NewPlayer();
	};
}

