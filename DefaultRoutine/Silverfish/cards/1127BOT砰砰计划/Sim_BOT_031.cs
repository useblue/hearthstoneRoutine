using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_031 : SimTemplate //* 地精炸弹 Goblin Bomb
//[x]<b>Deathrattle:</b> Deal 2 damageto the enemy hero.
//<b>亡语：</b>对敌方英雄造成2点伤害。 
	{
        
		
        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.minionGetDamageOrHeal(m.own ? p.enemyHero : p.ownHero, 2);
        }
	}
}