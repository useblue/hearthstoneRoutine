namespace HREngine.Bots
{
	class Sim_ULDA_605 : SimTemplate //* 便捷快餐 Fast Food
//Add all enemy minions to your Adventure Deck. They cost (1) less.
//将所有敌方随从加入你的冒险模式套牌。其法力值消耗减少（1）点。 
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