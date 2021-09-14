using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_301 : SimTemplate //* 上古之神护卫 Ancient Shieldbearer
//<b>Battlecry:</b> If your C'Thun has at least 10 Attack, gain 10 Armor.
//<b>战吼：</b>如果你的克苏恩具有至少10点攻击力，则获得10点护甲值。 
	{
		
		
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (own.own)
            {
                if (p.anzOgOwnCThunAngrBonus + 6 > 9) p.minionGetArmor(p.ownHero, 10);
                else p.evaluatePenality += 5;
            }
		}
	}
}