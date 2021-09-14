using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOT_534 : SimTemplate //* 镀金的石像鬼 Gilded Gargoyle
//<b>Deathrattle:</b> Add a Coin to your hand.
//<b>亡语：</b>将一个幸运币置入你的手牌。 
	{
		
		
        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.drawACard(CardDB.cardNameEN.thecoin, m.own);
        }
    }
}