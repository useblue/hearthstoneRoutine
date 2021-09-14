using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_060 : SimTemplate //* 青蛙之灵 Spirit of the Frog
//[x]<b>Stealth</b> for 1 turn.Whenever you cast a spell,draw a spell from your deckthat costs (1) more.
//<b>潜行</b>一回合。每当你施放一个法术，从你的牌库中抽取一张法力值消耗增加（1）点的法术牌。 
	{



        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            if (hc.card.type == CardDB.cardtype.SPELL && wasOwnCard == triggerEffectMinion.own)
            {
                p.drawACard(CardDB.cardNameEN.unknown, wasOwnCard);
            }

        }

	}
}