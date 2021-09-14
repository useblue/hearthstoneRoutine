using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	class Sim_AT_080 : SimTemplate //* 要塞指挥官 Garrison Commander
//You can use your Hero Power twice a turn.
//每个回合你可以使用两次英雄技能。 
	{
		
	
        public override void onAuraStarts(Playfield p, Minion own)
		{
            if (own.own)
            {
                bool another = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID) another = true;
                }
                if (!another) p.ownHeroPowerAllowedQuantity++;
            }
            else
            {
                bool another = false;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID) another = true;
                }
                if (!another) p.enemyHeroPowerAllowedQuantity++;
            }
		}

        public override void onAuraEnds(Playfield p, Minion own)
        {
            if (own.own)
            {
                bool another = false;
                foreach (Minion m in p.ownMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID) another = true;
                }
                if (!another) p.ownHeroPowerAllowedQuantity--;
                if (p.anzUsedOwnHeroPower >= p.ownHeroPowerAllowedQuantity) p.ownAbilityReady = false;
            }
            else
            {
                bool another = false;
                foreach (Minion m in p.enemyMinions)
                {
                    if (m.name == CardDB.cardNameEN.garrisoncommander && own.entitiyID != m.entitiyID) another = true;
                }
                if (!another) p.enemyHeroPowerAllowedQuantity--;
                if (p.anzUsedEnemyHeroPower >= p.enemyHeroPowerAllowedQuantity) p.enemyAbilityReady = false;
            }
        }
	}
}