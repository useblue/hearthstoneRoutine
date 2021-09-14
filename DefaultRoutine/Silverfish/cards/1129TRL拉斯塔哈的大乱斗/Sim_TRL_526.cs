using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_526 : SimTemplate //* 龙喉喷火者 Dragonmaw Scorcher
//<b>Battlecry:</b> Deal 1 damage to all other minions.
//<b>战吼：</b>对所有其他随从造成1点伤害。 
	{
		

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.allMinionsGetDamage(1, own.entitiyID);
        }
	}
}