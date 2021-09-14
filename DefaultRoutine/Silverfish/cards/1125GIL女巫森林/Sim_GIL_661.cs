using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_GIL_661 : SimTemplate //* 神圣赞美诗 Divine Hymn
//Restore #6 Health to all friendly characters.
//为所有友方角色恢复#6点生命值。
    {
        
        
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int heal = (ownplay) ? p.getSpellHeal(6) : p.getEnemySpellHeal(6) ;
            p.allCharsOfASideGetDamage(ownplay, -heal);
        }


        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}
