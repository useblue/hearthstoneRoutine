namespace HREngine.Bots
{
	class Sim_ULDA_019 : SimTemplate //* 运货驮骡 Pack Mule
//Reduce the Cost of your 10 starter cards by (1).
//使你初始10张卡牌的法力值消耗减少（1）点。 
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