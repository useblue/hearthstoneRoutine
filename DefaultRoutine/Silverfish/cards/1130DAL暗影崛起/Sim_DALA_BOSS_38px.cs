namespace HREngine.Bots
{
	class Sim_DALA_BOSS_38px : SimTemplate //* 传送门派对 Portal Party
//<b>Hero Power</b>Add a random Portal to your hand.
//<b>英雄技能</b>随机将一张传送门牌置入你的手牌。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_HAND_NOT_FULL),
            };
        }
	}
}