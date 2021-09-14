using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_032 : SimTemplate //* 林地树妖 Grove Tender
//<b>Choose One -</b> Give each player a Mana Crystal; or Each player draws a card.
//<b>抉择：</b>使每个玩家获得一个法力水晶；或每个玩家抽一张牌。 
    {

        

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (choice == 1 || (p.ownFandralStaghelm > 0 && own.own))
            {
				p.mana = Math.Min(10, p.mana+1);
				p.ownMaxMana = Math.Min(10, p.ownMaxMana+1);
				p.enemyMaxMana = Math.Min(10, p.enemyMaxMana+1);
            }

            if (choice == 2 || (p.ownFandralStaghelm > 0 && own.own))
            {
                p.drawACard(CardDB.cardIDEnum.None, true);
                p.drawACard(CardDB.cardIDEnum.None, false);
            }
        }


    }

}