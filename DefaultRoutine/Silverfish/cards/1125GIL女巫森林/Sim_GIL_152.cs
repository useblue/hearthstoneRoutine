using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GIL_152 : SimTemplate //* 黑嚎炮塔 Blackhowl Gunspire
//[x]Can't attack. Wheneverthis minion takes damage,deal 3 damage to arandom enemy.
//无法攻击。每当该随从受到伤害时，随机对一个敌人造成3点伤害。 
    {
        

        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            if (m.anzGotDmg > 0)
            {
                int tmp = m.anzGotDmg;
                m.anzGotDmg = 0;
                for (int i = 0; i < tmp; i++)
                {
					p.minionGetDamageOrHeal(m.own ? p.enemyHero : p.ownHero, 3);
				}
			}
        }
    }
}