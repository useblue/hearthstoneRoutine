using System;
using System.Collections.Generic;
using System.Text;


namespace HREngine.Bots
{ 

public class Sim_DAL_614: SimTemplate 
{ 



public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice) 
{ 
int dmg = 2; 
p.minionGetDamageOrHeal(target, dmg); 
} 

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
} 
}

