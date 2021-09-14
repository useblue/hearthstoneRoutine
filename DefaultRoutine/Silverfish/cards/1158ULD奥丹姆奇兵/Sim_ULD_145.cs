namespace HREngine.Bots
{
	class Sim_ULD_145 : SimTemplate //* 英勇狂热者 Brazen Zealot
	{
		//Whenever you summon a minion, gain +1 Attack.
		//每当你召唤一个随从，获得+1攻击力。
		public override void onMinionWasSummoned(Playfield p, Minion m, Minion summonedMinion)
        {
            if (m.entitiyID != summonedMinion.entitiyID && m.own == summonedMinion.own)
            {
				p.minionGetBuffed(m, 1, 0);
            }
        }


	}
}