using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_CORE_KAR_065 : SimTemplate //* Menagerie Warden
	{
		//Battlecry: Choose a friendly Beast. Summon a copy of it.
		
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            if (target != null && own.own && p.ownMinions.Count < 7)
            {
                int pos = p.ownMinions.Count;
                p.callKid(own.handcard.card, pos, own.own);
                p.ownMinions[pos].setMinionToMinion(target);
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 20),
            };
        }
    }
}