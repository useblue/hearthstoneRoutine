using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_ICC_481: SimTemplate //* 死亡先知萨尔 Thrall, Deathseer
//<b>Battlecry:</b> Transform your minions into random ones that cost (2) more.
//<b>战吼：</b>随机将你的所有随从变形成为法力值消耗增加（2）点的随从。 
    {
        
        
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.setNewHeroPower(CardDB.cardIDEnum.ICC_481p, ownplay); 
            if (ownplay) p.ownHero.armor += 5;
            else p.enemyHero.armor += 5;

            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
            foreach (Minion m in temp)
            {
                p.minionTransform(m, p.getRandomCardForManaMinion(m.handcard.card.cost + 2));
            }
        }
    }
}