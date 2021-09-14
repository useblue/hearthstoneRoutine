using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DMF_163 : SimTemplate //* 狂欢小丑 Carnival Clown
	{
		//[x]<b>Taunt</b><b>Battlecry:</b> Summon 2 copiesof this. <b>Corrupt:</b> Fill yourboard with copies.
		//<b>嘲讽，战吼：</b>召唤该随从的两个复制。<b>腐蚀：</b>召唤该随从的复制，直到你的随从数量达到上限。
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DMF_163);

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			int pos = p.ownMinions.Count ;
			p.callKid(kid, pos, own.own);
			p.callKid(kid, pos, own.own);
			foreach (Minion m in p.ownMinions)
			{
				if(m.handcard.card.cardIDenum == CardDB.cardIDEnum.DMF_163){
					p.minionSetAngrToX(m, own.Angr);
					p.minionSetLifetoX(m, own.Hp);					
				}
			}
		}
		
	}
}
