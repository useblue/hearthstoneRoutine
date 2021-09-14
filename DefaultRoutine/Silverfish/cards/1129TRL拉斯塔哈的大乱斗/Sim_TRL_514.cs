using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_514 : SimTemplate //* 好斗的侏儒 Belligerent Gnome
//[x]<b>Taunt</b><b>Battlecry:</b> If your opponenthas 2 or more minions,gain +1 Attack.
//<b>嘲讽</b><b>战吼：</b>如果你的对手拥有2个或者更多随从，便获得+1攻击力。 
	{
        
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{            
            if (p.enemyMinions.Count >= 2)
            {
               p.minionGetBuffed(own, 1, 0);
            }
		}
	}
}