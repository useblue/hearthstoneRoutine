using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DAL_355 : SimTemplate //* 织命者 Lifeweaver
//Whenever you restore Health, add a random Druid spell to your hand.
//每当有角色获得你的治疗时，随机将一张德鲁伊法术牌置入你的手牌。 
	{
        

        public override void onACharGotHealed(Playfield p, Minion triggerEffectMinion, int charsGotHealed)
        {
            for (int i = 0; i < charsGotHealed; i++)
            {
                p.drawACard(CardDB.cardNameEN.unknown, triggerEffectMinion.own);
            }
        }
	}
}