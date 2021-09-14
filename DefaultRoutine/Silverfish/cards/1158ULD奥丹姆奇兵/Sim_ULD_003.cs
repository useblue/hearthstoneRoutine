using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULD_003 : SimTemplate //* 了不起的杰弗里斯 Zephrys the Great
//<b>Battlecry:</b> If your deck has no duplicates, wish for the perfect card.
//<b>战吼：</b>如果你的牌库里没有相同的牌，则可以许愿获得一张完美的卡牌。 
	{
		
		
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            if (m.own && p.prozis.noDuplicates) p.drawACard(CardDB.cardNameEN.unknown, m.own);
        }
    }
}