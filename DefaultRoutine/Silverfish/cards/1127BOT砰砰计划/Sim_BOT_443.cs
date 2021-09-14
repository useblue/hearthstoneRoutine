using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_443 : SimTemplate //* 虚空分析师 Void Analyst
//<b>Deathrattle:</b> Give all Demons in your hand +1/+1.
//<b>亡语：</b>使你手牌中的所有恶魔牌获得+1/+1。 
	{
		
        public override void onDeathrattle(Playfield p, Minion m)
        {
            if (m.own)
            {
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if (hc.card.type == CardDB.cardtype.MOB)
                    {
                        hc.addattack++;
                        hc.addHp++;
                        p.anzOwnExtraAngrHp += 2;
                    }
                }
            }
        }
	}
}