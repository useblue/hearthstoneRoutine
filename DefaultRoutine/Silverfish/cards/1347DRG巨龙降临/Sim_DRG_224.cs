using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DRG_224 : SimTemplate //* 尼索格 Nithogg
	{
		//[x]<b>Battlecry:</b> Summon two0/3 Eggs. Next turn theyhatch into 4/4 Drakeswith <b>Rush</b>.
		//<b>战吼：</b>召唤两个0/3的龙卵。下个回合它们将孵化为4/4并具有<b>突袭</b>的幼龙。
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DRG_224t);//风暴龙卵
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			p.callKid(kid, own.zonepos, own.own);
			p.callKid(kid, own.zonepos, own.own);
		}
	}
}