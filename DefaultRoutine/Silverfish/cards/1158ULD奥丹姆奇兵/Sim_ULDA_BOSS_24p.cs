namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_24p : SimTemplate //* 腐化的绿洲 Corrupted Oasis
//[x]<b>Hero Power</b>Add two 2/2 Treantsto your hand.
//<b>英雄技能</b>将两张2/2的树人置入你的手牌。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_HAND_NOT_FULL),
            };
        }
	}
}