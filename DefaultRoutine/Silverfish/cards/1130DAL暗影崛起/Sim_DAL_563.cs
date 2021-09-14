using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DAL_563 : SimTemplate //* 性急的杂兵 Eager Underling
//<b>Deathrattle:</b> Give two random friendly minions +2/+2.
//<b>亡语：</b>随机使两个友方随从获得+2/+2。 
	{
		
		
        public override void onDeathrattle(Playfield p, Minion m)
        {
			Minion target = (m.own) ? p.searchRandomMinion(p.ownMinions, searchmode.searchLowestAttack) : p.searchRandomMinion(p.enemyMinions, searchmode.searchLowestAttack);
			if (target != null) p.minionGetBuffed(target, 2, 2);
			if (target != null) p.minionGetBuffed(target, 2, 2);
        }
    }
}