using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_FP1_022 : SimTemplate //* 空灵召唤者 Voidcaller
//<b>Deathrattle:</b> Put a random Demon from your hand into the battlefield.
//<b>亡语：</b>随机将一张恶魔牌从你的手牌置入战场。 
	{
        
        CardDB.Card c = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_301);

        public override void onDeathrattle(Playfield p, Minion m)
        {
            if (m.own)
            {
                List<Handmanager.Handcard> temp = new List<Handmanager.Handcard>();
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    if ((TAG_RACE)hc.card.race == TAG_RACE.DEMON)
                    {
                        temp.Add(hc);
                    }
                }

                temp.Sort((x, y) => x.card.Attack.CompareTo(y.card.Attack));

                foreach (Handmanager.Handcard mnn in temp)
                {
                    p.callKid(mnn.card, p.ownMinions.Count, true, false);
                    p.removeCard(mnn);
                    break;
                }
            }
            else
            {
                if (p.enemyAnzCards >= 1)
                {
                    p.callKid(c, p.enemyMinions.Count, false, false);
                }
            }
        }
	}
}