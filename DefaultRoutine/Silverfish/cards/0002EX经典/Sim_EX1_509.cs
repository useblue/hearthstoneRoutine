using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_EX1_509 : SimTemplate //* 鱼人招潮者 Murloc Tidecaller
	{
		//Whenever you summon a Murloc, gain +1 Attack.
		//每当你召唤一个鱼人，便获得+1攻击力。

        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            if (triggerEffectMinion.own == summonedMinion.own && (TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.MURLOC)
            {
                p.minionGetBuffed(triggerEffectMinion, 1, 0);
            }
        }

	}
}