using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CORE_UNG_020 : SimTemplate //* Arcanologist
	{
		//Battlecry: Draw a Secret from your deck.

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{

			p.drawACard(CardDB.cardIDEnum.EX1_594, own.own);
		}
	}
}