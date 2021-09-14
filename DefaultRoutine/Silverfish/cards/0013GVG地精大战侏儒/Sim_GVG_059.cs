using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    internal class Sim_GVG_059 : SimTemplate //* 齿轮光锤 Coghammer
//<b>Battlecry:</b> Give a random friendly minion <b>Divine Shield</b> and <b>Taunt</b>.
//<b>战吼：</b>随机使一个友方随从获得<b>圣盾</b>和<b>嘲讽</b>。 
    {
        
        private CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_059);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.equipWeapon(w, ownplay);
            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
            if (temp.Count <= 0) return;
            Minion m = p.searchRandomMinion(temp, searchmode.searchLowestHP);
            if (m != null)
            {
                m.divineshild = true;
                if (!m.taunt)
                {
                    m.taunt = true;
                    if (m.own) p.anzOwnTaunt++;
                    else p.anzEnemyTaunt++;
                }
            }
        }
    }
}