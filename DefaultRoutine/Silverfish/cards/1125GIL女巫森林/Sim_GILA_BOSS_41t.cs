namespace HREngine.Bots
{
	class Sim_GILA_BOSS_41t : SimTemplate //* 劈砍 Hack
//Deal $1 damage to a minion. Then do it four more times.
//对一名随从造成$1点伤害。再重复四次。 
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