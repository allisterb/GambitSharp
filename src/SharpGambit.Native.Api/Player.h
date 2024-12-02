#pragma once

#include "pch.h"
#include "games/game.h"

namespace SharpGambit
{
	class Player
	{
	private:
		Gambit::GamePlayer player;

	public:
		API Player(Gambit::GamePlayer);
	};
}

