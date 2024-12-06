#include "pch.h"
#include "sharpgambit.h"

namespace SharpGambit::Native::Tests
{ 

	TEST(GameApiTest, CanCreateGame) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto g = NewStrategicGame("foo", 2, pn, s);
	}
		

}