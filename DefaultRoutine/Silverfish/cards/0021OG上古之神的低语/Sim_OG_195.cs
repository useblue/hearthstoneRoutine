using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_OG_195 : SimTemplate //* 上古之神的小精灵 Wisps of the Old Gods
//<b>Choose One -</b> Summon seven 1/1 Wisps; or Give your minions +2/+2.
//<b>抉择：</b>召唤七个1/1的小精灵；或者使你的所有随从获得+2/+2。 
	{
		
		
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_231);
		
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                for (int i = 0; i < 7; i++)
                {
                    int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(kid, pos, ownplay);
                }
            }
            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                p.allMinionOfASideGetBuffed(ownplay, 2, 2);
            }
        }
    }
}