using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_TRL_058 : SimTemplate //* 亡鬼幻象 Haunting Visions
//The next spell you cast this turn costs (3) less. <b>Discover</b> a spell.
//在本回合中，你所施放的下一个法术的法力值消耗减少（3）点。<b>发现</b>一张法术牌。 
	{


		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
            if (ownplay)
            {
                p.playedPreparation = true;
				p.drawACard(CardDB.cardNameEN.frostbolt,ownplay, true);
            }
		}

	}
}