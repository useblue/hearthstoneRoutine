using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_SW_422b : SimTemplate //* 肥料滋养 Fertilizer
	{
		//Give your minions+1 Attack.
		//使你的所有随从获得+1攻击力。
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
			foreach (Minion m in temp)
			{
				p.minionGetBuffed(m, 1, 0);
			}

		}

	}
}