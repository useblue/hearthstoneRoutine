using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_504h : SimTemplate //* 蛊惑 Bewitch
//[x]<b>Passive Hero Power</b>After you play a minion,add a random Shamanspell to your hand.
//<b>被动英雄技能</b>在你使用一张随从牌后，随机将一张萨满祭司法术牌置入你的手牌。 
	{
		
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            if (triggerEffectMinion.own == summonedMinion.own)
            {
                p.drawACard(CardDB.cardNameEN.unknown, triggerEffectMinion.own);
            }
        }

	}
}