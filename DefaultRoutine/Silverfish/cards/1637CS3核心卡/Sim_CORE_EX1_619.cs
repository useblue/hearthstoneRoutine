using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CORE_EX1_619 : SimTemplate //* equality
	{
        //Change the Health of ALL minions to 1.

		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            foreach (Minion m in p.ownMinions)
            {
                p.minionSetLifetoX(m, 1);
            }
            foreach (Minion m in p.enemyMinions)
            {
                p.minionSetLifetoX(m, 1);
            }
		}
	}
}