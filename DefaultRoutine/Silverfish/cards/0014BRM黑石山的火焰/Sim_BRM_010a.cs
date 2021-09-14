using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BRM_010a : SimTemplate //* 火焰猎豹形态 Firecat Form
//
// 
	{
		
		
        CardDB.Card cat = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_010t);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.minionTransform(target, cat);
        }
	}
}