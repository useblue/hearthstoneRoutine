using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_EX1_083 : SimTemplate //* 工匠大师欧沃斯巴克 Tinkmaster Overspark
	{
		//[x]<b>Battlecry:</b> Transformanother random minioninto a 5/5 Devilsaur or a 1/1 Squirrel.
		//<b>战吼：</b>随机使另一个随从变形成为一个5/5的魔暴龙或一个1/1的松鼠。
		
        CardDB.Card card1 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_tk29); // rex
        CardDB.Card card2 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_tk28); // squirrel
        //todo better
//    kampfschrei:/ verwandelt einen anderen zufälligen diener in einen teufelssaurier (5/5) oder ein eichhörnchen (1/1).
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
            int oc = p.ownMinions.Count;
            int ec = p.enemyMinions.Count;
            if (oc == 0 && ec == 0) return;
            if (oc > ec)
            {
                List<Minion> temp = new List<Minion>(p.ownMinions);
                temp.AddRange(p.enemyMinions);
                temp.Sort((a, b) => a.Hp.CompareTo(b.Hp));//transform the weakest
                foreach (Minion m in temp)
                {
                    p.minionTransform(m, card1);
                    break;
                }
            }
            else
            {
                List<Minion> temp = new List<Minion>(p.ownMinions);
                temp.AddRange(p.enemyMinions);
                temp.Sort((a, b) => -a.Hp.CompareTo(b.Hp));//transform the strongest
                foreach (Minion m in temp)
                {
                    p.minionTransform(m, card2);
                    break;
                }
            }
		}


	}
}