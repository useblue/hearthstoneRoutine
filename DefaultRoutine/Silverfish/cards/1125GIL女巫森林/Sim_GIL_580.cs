using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_580 : SimTemplate //* 城镇公告员 Town Crier
//<b>Battlecry:</b> Draw a <b>Rush</b> minion from your deck.
//<b>战吼：</b>从你的牌库中抽一张具有<b>突袭</b>的随从牌。 
	{


		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            p.drawACard(CardDB.cardNameEN.unknown, own.own);
		}


	}
}