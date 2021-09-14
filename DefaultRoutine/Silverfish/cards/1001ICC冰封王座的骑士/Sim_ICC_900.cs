using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_ICC_900: SimTemplate //* 死灵恶鬼 Necrotic Geist
//Whenever one of your other minions dies, summon a 2/2 Ghoul.
//每当你的其他随从死亡时，召唤一个2/2的食尸鬼。 
    {
        

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ICC_900t); 

        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            int diedMinions = (m.own) ? p.tempTrigger.ownMinionsDied : p.tempTrigger.enemyMinionsDied;
            if (diedMinions == 0) return;
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;
            m.pID = p.pID;
            m.extraParam2 = diedMinions;
            for (int i = 0; i < residual; i++)
            {
                p.callKid(kid, m.zonepos, m.own);
            }
        }
    }
}