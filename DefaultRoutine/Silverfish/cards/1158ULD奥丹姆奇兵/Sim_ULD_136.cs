using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_ULD_136 : SimTemplate //* 不虚此行 Worthy Expedition
//<b>Discover</b> a <b>Choose One</b> card.
//<b>发现</b>一张<b>抉择</b>牌。 
	{
        

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.drawACard(CardDB.cardNameEN.unknown, ownplay);
        }
    }
}