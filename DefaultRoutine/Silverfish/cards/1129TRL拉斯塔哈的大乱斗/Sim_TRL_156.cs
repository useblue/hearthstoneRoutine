using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_156 : SimTemplate //* 盗取武器 Stolen Steel
//<b>Discover</b> a weapon <i>(from another class)</i>.
//<b>发现</b>一张<i>（其他职业的）</i>武器牌。 
	{
		
		
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.fierywaraxe, ownplay);
		}
	}
}