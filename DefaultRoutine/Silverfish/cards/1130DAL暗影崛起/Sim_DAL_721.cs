using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DAL_721 : SimTemplate //* 亡者卡特琳娜 Catrina Muerte
//[x]At the end of your turn,summon a friendly minionthat died this game.
//在你的回合结束时，召唤一个在本局对战中死亡的友方随从。 
	{
		
		
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_623); 

        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            foreach (GraveYardItem gyi in p.diedMinions.ToArray())
            {
                if (gyi.own == triggerEffectMinion.own)
                {
                    CardDB.Card card = CardDB.Instance.getCardDataFromID(gyi.cardid);
                    int pos = triggerEffectMinion.own ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(card, p.ownMinions.Count, gyi.own);
                }
            }
        }
	}
}