using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_LOOT_365 : SimTemplate //* 宝石魔像 Gemstudded Golem
//<b>Taunt</b>Can only attack if you have 5 or more Armor.
//<b>嘲讽</b>除非你的护甲值大于或等于5点，否则无法进行攻击。 
	{
		
		
		public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            if (!m.silenced)
            {
                m.cantAttack = (p.ownHero.armor < 5) ? true : false;
                m.updateReadyness();
            }
        }
	}
}