namespace HREngine.Bots
{
	class Sim_DALA_BOSS_01p : SimTemplate //* 咀嚼 Chomp
//<b>Hero Power</b><b>Lifesteal</b>Deal 1 damage to a minion.
//<b>英雄技能</b><b>吸血</b>，对一个随从造成1点伤害。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}