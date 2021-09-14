using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_601 : SimTemplate //* 气象学家 Meteorologist
//<b>Battlecry:</b> For each card in your hand, deal 1 damage to a random enemy.
//<b>战吼：</b>你每有一张手牌，便随机对一个敌人造成1点伤害。 
	{


		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			int dmg = (own.own) ? p.owncards.Count : p.enemyAnzCards;
            p.minionGetDamageOrHeal(target, dmg);
		}


	}
}