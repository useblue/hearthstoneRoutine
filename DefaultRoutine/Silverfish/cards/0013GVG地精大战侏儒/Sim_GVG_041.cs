using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GVG_041 : SimTemplate //* 黑暗私语 Dark Wispers
//<b>Choose One -</b> Summon 5 Wisps; or Give_a minion +5/+5 and <b>Taunt</b>.
//<b>抉择：</b>召唤5个小精灵；或者使一个随从获得+5/+5和<b>嘲讽</b>。 
    {
        

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_231);
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                for (int i = 0; i < 5; i++)
                {
                    int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
                    p.callKid(kid, pos, ownplay);
                }
            }
            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                if (target != null)
                {
                    p.minionGetBuffed(target, 5, 5);
                    if (!target.taunt)
                    {
                        target.taunt = true;
                        if (target.own) p.anzOwnTaunt++;
                        else p.anzEnemyTaunt++;
                    }
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }

}