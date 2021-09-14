using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_BRM_004 : SimTemplate //* 暮光雏龙 Twilight Whelp
//<b>Battlecry:</b> If you're holding a Dragon, gain +2 Health.
//<b>战吼：</b>如果你的手牌中有龙牌，便获得+2生命值。 
    {
        

        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
			if(m.own)
			{
				bool dragonInHand = false;
				foreach (Handmanager.Handcard hc in p.owncards)
				{
					if ((TAG_RACE)hc.card.race == TAG_RACE.DRAGON)
					{
						dragonInHand = true;
						break;
					}
				}
				if(dragonInHand) p.minionGetBuffed(m, 0, 2);
			}
			else
			{
                if (p.enemyAnzCards >= 2) p.minionGetBuffed(m, 0, 2);
			}
        }
    }
}