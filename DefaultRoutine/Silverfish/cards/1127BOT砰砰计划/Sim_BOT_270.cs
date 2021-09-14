using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_BOT_270 : SimTemplate //* 欢乐的发明家 Giggling Inventor
//<b>Battlecry:</b> Summon two 1/2 Mechs with <b>Taunt</b> and_<b>Divine Shield</b>.
//<b>战吼：</b>召唤两个1/2并具有<b>嘲讽</b>和<b>圣盾</b>的机械。 
	{
        
		
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_085);
		
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.callKid(kid, own.zonepos, own.own);
            p.callKid(kid, own.zonepos - 1, own.own);
        }
	}
}