using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DAL_413: SimTemplate //* 怪盗征募员 EVIL Conscripter
//<b>Deathrattle:</b> Add a <b>Lackey</b> to your hand.
//<b>亡语：</b>将一张<b>跟班</b>牌置入你的手牌。 
    {
        
        
        public override void onDeathrattle(Playfield p, Minion m)
        {           
            p.drawACard(CardDB.cardNameEN.unknown, m.own, true);
        }
    }
}