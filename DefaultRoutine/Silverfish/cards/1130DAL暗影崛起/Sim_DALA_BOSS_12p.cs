namespace HREngine.Bots
{
	class Sim_DALA_BOSS_12p : SimTemplate //* 禁忌之爱 Forbidden Love
//<b>Hero Power</b>Destroy a random minion for each player.
//<b>英雄技能</b>随机消灭双方玩家的一个随从。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_TOTAL_MINIONS, 1),
            };
        }
	}
}