using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_VAN_EX1_062 : SimTemplate //* 老瞎眼 Old Murk-Eye
//<b>Charge</b>. Has +1 Attack for each other Murloc on the battlefield.
//<b>冲锋</b>，在战场上每有一个其他鱼人便获得+1攻击力。 
	{
        

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            foreach (Minion m in p.ownMinions)
            {
                if (m.handcard.card.race == CardDB.Race.MURLOC)
                {
                    if (m.entitiyID != own.entitiyID) p.minionGetBuffed(own, 1, 0);
                }
            }

            foreach (Minion m in p.enemyMinions)
            {
                if (m.handcard.card.race == CardDB.Race.MURLOC)
                {
                    if (m.entitiyID != own.entitiyID) p.minionGetBuffed(own, 1, 0);
                }
            }
		}

        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            if (summonedMinion.handcard.card.race == CardDB.Race.MURLOC)
            {
                p.minionGetBuffed(triggerEffectMinion, 1, 0);
            }
        }

        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            int diedMinions = p.tempTrigger.ownMurlocDied + p.tempTrigger.enemyMurlocDied;
            if (diedMinions == 0) return;
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;
            m.pID = p.pID;
            m.extraParam2 = diedMinions;
            if (residual >= 1)
			{
                p.minionGetBuffed(m, -1 * residual, 0);
			}
        }

	}
}