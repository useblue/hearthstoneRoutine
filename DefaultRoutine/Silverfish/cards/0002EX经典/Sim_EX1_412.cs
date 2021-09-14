using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_EX1_412 : SimTemplate //* 暴怒的狼人 Raging Worgen
	{
		//Has +1 Attack and <b>Windfury</b> while damaged.
		//受伤时具有+1攻击力和<b>风怒</b>。
        public override void onEnrageStart(Playfield p, Minion m)
        {
            m.Angr++;
            p.minionGetWindfurry(m);
        }

        public override void onEnrageStop(Playfield p, Minion m)
        {
            m.Angr--;
            m.windfury = false;
            if (m.numAttacksThisTurn == 1) m.Ready = false;
        }


	}
}