using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_237 : SimTemplate //* 铍金毁灭者 Beryllium Nullifier
//<b>Magnetic</b>Can't be targeted by spells or Hero Powers.
//<b>磁力</b>无法成为法术或英雄技能的目标。 
	{


        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            own.cantBeTargetedBySpellsOrHeroPowers = true;
        }
	}
}