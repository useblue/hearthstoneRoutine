using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_226 : SimTemplate //* 虚魂破坏者 Nethersoul Buster
//<b>Battlecry:</b> Gain +1 Attack for each damage your hero has taken this turn.
//<b>战吼：</b>在本回合中，你的英雄每受到一点伤害，便获得+1攻击力。 
	{
		
		
		public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int anzOwnHeroGotDmg)
		{
			if (anzOwnHeroGotDmg >= 1 )
			{	
                p.minionGetBuffed(m, anzOwnHeroGotDmg, 0);
			}
		}
	}
}