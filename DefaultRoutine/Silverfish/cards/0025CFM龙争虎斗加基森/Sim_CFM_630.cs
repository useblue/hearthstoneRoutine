using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CFM_630 : SimTemplate //* 伪造的幸运币 Counterfeit Coin
//Gain 1 Mana Crystal this turn only.
//在本回合中，获得一个法力水晶。 
	{
		

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.mana = Math.Min(p.mana + 1, 10);
        }
    }
}