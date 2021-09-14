using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DAL_088 : SimTemplate //* 机械保险箱 Safeguard
//[x]<b>Taunt</b><b>Deathrattle:</b> Summon a 0/5Vault Safe with <b>Taunt</b>.
//<b>嘲讽，亡语：</b>召唤一个0/5并具有<b>嘲讽</b>的保险柜。 
	{
		
		
        CardDB.Card c = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.DAL_088t2);
		
        public override void onDeathrattle(Playfield p, Minion m)
        {
            p.callKid(c, m.zonepos - 1, m.own);
        }
	}
}