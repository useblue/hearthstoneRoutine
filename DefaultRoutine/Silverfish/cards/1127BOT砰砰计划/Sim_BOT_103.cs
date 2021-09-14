using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_103 : SimTemplate //* 观星者露娜 Stargazer Luna
//After you play theright-most card in your hand, draw a card.
//在你使用最右边的一张手牌后，抽一张牌。 
	{
		

        public override void onAuraStarts(Playfield p, Minion m)
		{
            if (m.own) p.anzOwnAviana++;
		}

        public override void onAuraEnds(Playfield p, Minion m)
        {
            if (m.own) p.anzOwnAviana--;
        }
	}
}