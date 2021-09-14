using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TRL_551 : SimTemplate //* 粗暴的恐怖巨魔 Reckless Diretroll
//<b>Taunt</b><b>Battlecry:</b> Discard your lowest Cost card.
//<b>嘲讽，战吼：</b>弃掉你手牌中法力值消耗最低的牌。 
    {
        
		
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.discardCards(1, own.own);
        }
    }
}