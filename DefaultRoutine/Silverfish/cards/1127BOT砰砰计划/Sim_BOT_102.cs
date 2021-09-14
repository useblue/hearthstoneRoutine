using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_102 : SimTemplate //* 火花钻机 Spark Drill
//<b>Rush</b><b>Deathrattle:</b> Add two1/1 Sparks with <b>Rush</b> to your hand.
//<b>突袭，亡语：</b>将两张1/1并具有<b>突袭</b>的“火花”置入你的手牌。 
	{
		

		public override void onDeathrattle(Playfield p, Minion m)
        {
            p.drawACard(CardDB.cardIDEnum.BOT_102t, m.own, true);
            p.drawACard(CardDB.cardIDEnum.BOT_102t, m.own, true);
        }
	}
}