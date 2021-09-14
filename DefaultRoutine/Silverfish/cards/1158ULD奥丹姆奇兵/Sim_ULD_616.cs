using System;
using System.Collections.Generic;
using System.Text;


namespace HREngine.Bots
{ 

    public class Sim_ULD_616 : SimTemplate 
    { 

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice) 
        { 
            if (target != null) 
            { 
                p.minionGetBuffed(target, 0, 2); 
                if (!target.taunt) 
                { 
                    target.taunt = true; 
                    if (target.own) 
                    { 
                        p.anzOwnTaunt++; 
                    } 
                    else 
                    { 
                        p.anzEnemyTaunt++; 
                    } 
                } 
            } 
        } 

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
    } 
}
