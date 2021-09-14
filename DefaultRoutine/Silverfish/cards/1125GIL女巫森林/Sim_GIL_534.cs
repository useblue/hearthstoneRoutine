using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_534 : SimTemplate //* 荆棘帮暴徒 Hench-Clan Thug
//After your hero attacks, give this minion +1/+1.
//在你的英雄攻击后，该随从获得+1/+1。 
	{
		
        public override void onHeroattack(Playfield p, Minion triggerMinion, Minion target, Minion hero)
        {
            if (triggerMinion.own == hero.own) p.minionGetBuffed(triggerMinion, 1, 1);
        }
	}
}