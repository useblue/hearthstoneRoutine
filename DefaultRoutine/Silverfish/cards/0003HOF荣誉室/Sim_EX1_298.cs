using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_EX1_298 : SimTemplate //* 炎魔之王拉格纳罗斯 Ragnaros the Firelord
//Can't attack. At the end of your turn, deal 8 damage to a random enemy.
//无法攻击。在你的回合结束时，随机对一个敌人造成8点伤害。 
    {
        

        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                Minion target = null;

                if (turnEndOfOwner)
                {
                    target = p.getEnemyCharTargetForRandomSingleDamage(8);
                }
                else
                {
                    target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack); 
                    if (target == null) target = p.ownHero;
                }
                p.minionGetDamageOrHeal(target, 8, true);
                triggerEffectMinion.stealth = false;
            }
        }
    }
}