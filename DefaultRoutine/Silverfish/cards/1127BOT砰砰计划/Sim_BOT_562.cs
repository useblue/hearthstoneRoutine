using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_BOT_562 : SimTemplate //* 伪装机器人 Coppertail Imposter
//<b>Battlecry:</b> Gain <b>Stealth</b> until your next turn.
//<b>战吼：</b>获得<b>潜行</b>直到你的下个回合。 
	{
        

		public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
		{
            m.stealth = true;
            m.conceal = true;
		}
	}
}