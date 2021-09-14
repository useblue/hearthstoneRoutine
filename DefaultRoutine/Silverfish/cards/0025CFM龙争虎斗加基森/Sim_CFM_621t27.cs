using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CFM_621t27 : SimTemplate //* 冰盖草 Icecap
//<b>Freeze</b> 3 random enemy minions.
//随机<b>冻结</b>三个敌方随从。 
	{
		

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			List<Minion> temp = (ownplay) ? p.enemyMinions : p.ownMinions;
			if (temp.Count > 3)
			{
				int anz = 0;
				target = p.searchRandomMinion(temp, searchmode.searchLowestHP);
                if (target != null && !target.frozen)
                {
                    p.minionGetFrozen(target);
					anz++;
				}
				foreach (Minion m in temp)
				{
					if (!m.frozen)
                    {
                        p.minionGetFrozen(m);
						anz++;
						if (anz > 2) break;
					}
				}
			}
			else
			{
				foreach (Minion m in temp)
                {
                    p.minionGetFrozen(m);
				}
			}
        }
    }
}