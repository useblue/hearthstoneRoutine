using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_525 : SimTemplate //* 竞技场财宝箱 Arena Treasure Chest
//<b>Deathrattle:</b> Draw 2 cards.
//<b>亡语：</b>抽两张牌。 
	{



        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.drawACard(CardDB.cardNameEN.unknown, m.own);
			p.drawACard(CardDB.cardNameEN.unknown, m.own);
        }

	}
}