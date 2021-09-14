using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_DMF_107: SimTemplate //* Frozen Clone
    {
        // Secret: After your opponent plays a minion, add two copies of it to your hand.

        public override void onSecretPlay(Playfield p, bool ownplay, int number)
        {
            p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
            p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
			p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
        }
    }
}