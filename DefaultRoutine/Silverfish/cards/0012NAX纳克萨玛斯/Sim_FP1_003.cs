using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_FP1_003 : SimTemplate //* 分裂软泥怪 Echoing Ooze
//<b>Battlecry:</b> Summon an exact copy of this minion at the end of the turn.
//<b>战吼：</b>在回合结束时召唤一个该随从的复制。 
	{
        

        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (triggerEffectMinion.playedThisTurn && triggerEffectMinion.own == turnEndOfOwner)
            {
                p.callKid(triggerEffectMinion.handcard.card, triggerEffectMinion.zonepos, turnEndOfOwner);
                List<Minion> temp = (turnEndOfOwner) ? p.ownMinions : p.enemyMinions;
                foreach (Minion mnn in temp)
                {
                    if (mnn.name == CardDB.cardNameEN.echoingooze && triggerEffectMinion.entitiyID != mnn.entitiyID)
                    {
                        mnn.setMinionToMinion(triggerEffectMinion);
                        break;
                    }
                }
            }
        }

	}
}