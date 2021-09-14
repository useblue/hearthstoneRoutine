using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_543 : SimTemplate //* 欧米茄灵能者 Omega Mind
//[x]<b>Battlecry:</b> If you have 10Mana Crystals, your spells have <b>Lifesteal</b> this turn.
//<b>战吼：</b>如果你有十个法力水晶，在本回合中你的所有法术具有<b>吸血</b>。 
	{


		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			if (own.own) 
			{
				if (p.ownMaxMana ==10)
				{
					target.ancestralspirit++;
				}
			}
		}

	}
}