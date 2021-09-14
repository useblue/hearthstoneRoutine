using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_042 : SimTemplate //* 亚煞极 Y'Shaarj, Rage Unbound
//At the end of your turn, put a minion from your deck into the battlefield.
//在你的回合结束时，将一个随从从你的牌库置入战场。 
	{
		
		
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_623); 

        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
				int pos = (triggerEffectMinion.own) ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(kid, pos, triggerEffectMinion.own, false);
				if (triggerEffectMinion.own)p.ownDeckSize--;
                else p.enemyDeckSize--;
            }
        }
	}
}