using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TRL_501 : SimTemplate //* 奥金尼幻象 Auchenai Phantasm
//<b>Battlecry:</b> This turn, your healing effects deal damage instead.
//<b>战吼：</b>在本回合中，你的治疗效果转而造成等量的伤害。 
    {
        

		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{			
            if (own.own)
            {
                p.embracetheshadow++;
            }
		}
	}
}