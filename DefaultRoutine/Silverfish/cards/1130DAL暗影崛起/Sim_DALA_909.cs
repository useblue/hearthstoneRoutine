namespace HREngine.Bots
{
	class Sim_DALA_909 : SimTemplate //* 统统滚蛋 You're All Fired!
//Remove all friendly minions in play from your Adventure Deck.
//将场上的所有友方随从移出你的冒险模式套牌。 
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