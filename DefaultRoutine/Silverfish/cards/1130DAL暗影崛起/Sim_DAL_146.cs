using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DAL_146: SimTemplate //* 青铜传令官 Bronze Herald
//<b>Deathrattle:</b> Add two 4/4 Dragons to your hand.
//<b>亡语：</b>将两张4/4的“青铜龙”置入你的手牌。 
    {
        
        
        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.drawACard(CardDB.cardIDEnum.DAL_146t, m.own, true); 
            p.drawACard(CardDB.cardIDEnum.DAL_146t, m.own, true);
        }
    }
}