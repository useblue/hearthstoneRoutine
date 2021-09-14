using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_GIL_529t : SimTemplate //* 幻术士 Spellshifter
//[x]<b>Spell Damage +1</b>Each turn this is in your hand,swap its Attack and Health.
//<b>法术伤害+1</b>如果这张牌在你的手牌中，每个回合使其攻击力和生命值互换。 
	{


        public override void onAuraStarts(Playfield p, Minion own)
		{
            if (own.own)
            {
                p.spellpower++;
            }
            else
            {
                p.enemyspellpower++;
            }
		}

        public override void onAuraEnds(Playfield p, Minion m)
        {

            if (m.own)
            {
                p.spellpower--;
            }
            else
            {
                p.enemyspellpower--;
            }
        }

	}
}