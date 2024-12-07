#include "pch.h"
#include "sharpgambit.h"

namespace SharpGambit::Native::Tests
{ 
	TEST(GameApiTest, CanCreateGame) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto gp = NewStrategicGame("foo", 2, pn, s);

		ASSERT_FALSE(gp == nullptr);
		auto gr = grep(gp);
		ASSERT_EQ("foo", gr->GetTitle());
		ASSERT_EQ(2, gr->NumPlayers());
		ASSERT_EQ(12, gr->NumOutcomes());
	}
		

}