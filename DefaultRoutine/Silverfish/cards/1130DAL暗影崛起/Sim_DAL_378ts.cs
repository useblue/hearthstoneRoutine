using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DAL_378ts: SimTemplate //* 猛兽出笼 Unleash the Beast
//Summon a 5/5 Wyvern with <b>Rush</b>.
//召唤一只5/5并具有<b>突袭</b>的双足飞龙。 
    {
        

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DAL_378t1);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            
            int place = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
			
			p.callKid(kid, place, ownplay, false);
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
    }
}