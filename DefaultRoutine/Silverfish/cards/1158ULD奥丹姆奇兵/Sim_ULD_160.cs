using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULD_160 : SimTemplate //* 邪恶交易 Sinister Deal
//<b>Discover</b> a <b>Lackey</b>.
//<b>发现</b>一张<b>跟班</b>牌。 
	{
		
		
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.unknown, ownplay);          
		}

	}
}