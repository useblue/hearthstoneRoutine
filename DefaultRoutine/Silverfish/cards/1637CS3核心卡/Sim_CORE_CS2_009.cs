using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_CORE_CS2_009 : SimTemplate //* Mark of the Wild
    {
        //Give a minion Taunt and +2/+2. (+2 Attack/+2 Health)

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.minionGetBuffed(target, 2, 2);
            if (!target.taunt)
            {
                target.taunt = true;
                if (target.own) p.anzOwnTaunt++;
                else p.anzEnemyTaunt++;
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}
