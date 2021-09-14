using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_ULD_289 : SimTemplate //* 鱼人投手 Fishflinger
	{
		//<b>Battlecry:</b> Add arandom Murloc to each player's hand.
		//<b>战吼：</b>将一张随机鱼人牌分别置入每个玩家的手牌。
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			p.drawACard(CardDB.cardIDEnum.None, true);
			p.drawACard(CardDB.cardIDEnum.None, false);
		}
	}
}