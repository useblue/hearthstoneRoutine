namespace HREngine.Bots
{
	class Sim_ULDA_024 : SimTemplate //* 高级驮骡 Upgraded Pack Mule
//Reduce the Cost of all cards in your deck by (1).
//使你牌库中所有卡牌的法力值消耗减少（1）点。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}