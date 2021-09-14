using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_DAL_538 : SimTemplate //* 隐秘破坏者 Unseen Saboteur
//<b>Battlecry:</b> Your opponent casts a random spell from their hand <i>(targets chosen randomly)</i>.
//<b>战吼：</b>随机使你的对手从手牌中施放一个法术<i>（目标随机而定）</i>。 
	{
		

        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_066); 

        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            int zonepos = (m.own) ? p.enemyMinions.Count : p.ownMinions.Count;
            p.callKid(kid, zonepos, !m.own);
        }
    }
}