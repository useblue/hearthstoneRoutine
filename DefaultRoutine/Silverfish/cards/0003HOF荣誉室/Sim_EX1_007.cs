using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_EX1_007 : SimTemplate //* 苦痛侍僧 Acolyte of Pain
//Whenever this minion takes damage, draw a_card.
//每当该随从受到伤害，抽一张牌。
    {
        

        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            if (m.anzGotDmg > 0)
            {
                int tmp = m.anzGotDmg;
                m.anzGotDmg = 0;
                for (int i = 0; i < tmp; i++)
                {
                    p.drawACard(CardDB.cardIDEnum.None, m.own);
                }
            }
        }
    }
}
