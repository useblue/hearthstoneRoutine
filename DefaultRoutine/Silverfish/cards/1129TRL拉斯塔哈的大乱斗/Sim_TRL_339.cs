using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_339 : SimTemplate //* 主人的召唤 Master's Call
//<b>Discover</b> a minion in your deck.If all 3 are Beasts,draw them all.
//从你的牌库中<b>发现</b>一张随从牌。如果三张牌都是野兽，则抽取全部三张牌。 
	{



		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.unknown, ownplay);
            p.drawACard(CardDB.cardNameEN.unknown, ownplay);
			p.drawACard(CardDB.cardNameEN.unknown, ownplay);
		}

	}
}