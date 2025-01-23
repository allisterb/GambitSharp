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
		
	TEST(GameApiTest, CanCreatePureStrategyProfile) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto gp = NewNormalFormGame("foo", 2, pn, s);
		gprep(GetPlayer(gp, 2))->GetStrategy(3)->SetLabel("bar");
		ASSERT_FALSE(gp == nullptr);
		auto psp_ = grep(gp)->NewPureStrategyProfile();
		auto psp = PSP_New(gp);
		//auto s1 = grep(gp)->GetPlayer(2)->GetStrategy(3);
		auto s1 = GetPlayerStrategy(GetPlayer(gp, 2), 3);
		psp_->SetStrategy(gsrep(s1));
		//auto x = gsrep(gsptr(s1));
		//auto y = psprep(pspptr(tt));
		//PSP_SetStrategy(psp, gsptr(s1));
		//y->SetStrategy(gsrep(grep(gp)->GetPlayer(2)->GetStrategy(3)));
		PSP_SetStrategy(psp, s1);
		auto str_ = psp_->GetStrategy(2);
		auto str = (*psprep(psp))->GetStrategy(grep(gp)->GetPlayer(2));
		ASSERT_EQ("bar", str_->GetLabel());
		ASSERT_EQ("bar", str->GetLabel());
	}

	TEST(GameApiTest, CanCreateMixedStrategyProfile) {
		const char* pn[] = { "bar", "baz" };
		int s[] = { 3, 4 };
		auto gp = NewNormalFormGame("foo", 2, pn, s);
		gprep(GetPlayer(gp, 2))->GetStrategy(3)->SetLabel("bar");
		ASSERT_FALSE(gp == nullptr);
		auto msp_ = grep(gp)->NewMixedStrategyProfile(0.0);
		auto mpsp = MSP_New(gp);
		//auto s1 = grep(gp)->GetPlayer(2)->GetStrategy(3);
		auto s1 = GetPlayerStrategy(GetPlayer(gp, 2), 3);
		ASSERT_EQ(0.25, msp_[gsrep(s1)]) ;
		msp_[gsrep(s1)] = 0.3;
		ASSERT_EQ(0.3, msp_[gsrep(s1)]);
		//ASSERT_EQ(0.3, msp_[gsrep(GetPlayerStrategy(GetPlayer(gp, 2), 4))]);
		MSP_SetStrategyProbability(mpsp, s1, 0.3);
		ASSERT_EQ(0.3, MSP_GetStrategyProbability(mpsp, s1));
		//msp_.
		//auto x = gsrep(gsptr(s1));
		//auto y = psprep(pspptr(tt));
		//PSP_SetStrategy(psp, gsptr(s1));
		//y->SetStrategy(gsrep(grep(gp)->GetPlayer(2)->GetStrategy(3)));
		//PSP_SetStrategy(psp, s1);
		//auto str_ = psp_->GetStrategy(2);
		//auto str = (*psprep(psp))->GetStrategy(grep(gp)->GetPlayer(2));
		//ASSERT_EQ("bar", str_->GetLabel());
		//ASSERT_EQ("bar", str->GetLabel());
	}

	TEST(GameApiTest, CanSolverPureStrategyProfile) {
		const char* pn[] = { "Player 1", "Player 2" };
		int s[] = { 2, 2 };
		auto gp = NewNormalFormGame("Prisoner's Dilemna", 2, pn, s);
		ASSERT_FALSE(gp == nullptr);
		auto psp = grep(gp)->NewPureStrategyProfile();
		auto go = grep(gp)->NewOutcome();
		//psp->SetOutcome()
	}
}