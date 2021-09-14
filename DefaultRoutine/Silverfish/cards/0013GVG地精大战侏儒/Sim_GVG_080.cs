using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_080 : SimTemplate //* 尖牙德鲁伊 Druid of the Fang
//<b>Battlecry:</b> If you have a Beast, transform this minion into a 7/7.
//<b>战吼：</b>如果你控制任何野兽，将该随从变形成为7/7。 
    {
        
        CardDB.Card betterguy = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_080t);

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;
            bool hasbeast = false;
            foreach (Minion m in temp)
            {
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.PET)
                {
                    hasbeast = true;
                    break;
                }
            }
            if(hasbeast) p.minionTransform(own, betterguy);
        }
    }
}