namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_12p : SimTemplate //* 添砖加瓦 Brick by Brick
//[x]<b>Hero Power</b><b>Discover</b> an Elemental.It costs (2) less.
//<b>英雄技能</b><b>发现</b>一张元素牌，其法力值消耗减少（2）点。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_HAND_NOT_FULL),
            };
        }
	}
}