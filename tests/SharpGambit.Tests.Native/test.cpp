#include "pch.h"
#include "sharpgambit.h"

namespace SharpGambit::Native::Tests
{ 
	TEST(GameApiTest, CanCreateGame) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto gp = NewNormalFormGame("foo", 2, pn, s);

		ASSERT_FALSE(gp == nullptr);
		auto gr = grep(gp);
		ASSERT_EQ("foo", gr->GetTitle());
		ASSERT_EQ(2, gr->NumPlayers());
		ASSERT_EQ(12, gr->NumOutcomes());
		ASSERT_EQ(3, gr->GetPlayer(1)->NumStrategies());
		ASSERT_EQ(4, gr->GetPlayer(2)->NumStrategies());

	}
		
	TEST(GameApiTest, CanCreateStrategyProfile) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto gp = NewNormalFormGame("foo", 2, pn, s);
		gprep(GetPlayer(gp, 2))->GetStrategy(3)->SetLabel("bar");
		ASSERT_FALSE(gp == nullptr);
		auto tt = gtablerep(gp)->NewPureStrategyProfile();
		auto psp = PSP_New(gp);
		auto s1 = grep(gp)->GetPlayer(2)->GetStrategy(3);
		auto x = gsrep(gsptr(s1));
		auto y = psprep(pspptr(tt));
		//PSP_SetStrategy(psp, gsptr(s1));
		y->SetStrategy(gsrep(grep(gp)->GetPlayer(2)->GetStrategy(3)));

		auto str = psprep(psp)->GetStrategy(grep(gp)->GetPlayer(2));
		ASSERT_EQ("bar", str->GetLabel());

	}
}