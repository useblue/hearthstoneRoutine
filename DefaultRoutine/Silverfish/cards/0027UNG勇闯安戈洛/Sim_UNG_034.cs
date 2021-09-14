using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_UNG_034 : SimTemplate //* 光照元素 Radiant Elemental
//Your spells cost (1) less.
//你的法术的法力值消耗减少（1）点。 
	{
		

        public override void onAuraStarts(Playfield p, Minion own)
        {
            if (own.own) p.ownSpelsCostMore--;
        }

        public override void onAuraEnds(Playfield p, Minion own)
        {
            if (own.own) p.ownSpelsCostMore++;
        }
	}
}