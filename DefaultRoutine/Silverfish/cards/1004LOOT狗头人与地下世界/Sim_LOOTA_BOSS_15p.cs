namespace HREngine.Bots
{
	class Sim_LOOTA_BOSS_15p : SimTemplate //* 圣化 Divinity
//<b>Hero Power</b>Give all your minions <b>Divine Shield</b>.
//<b>英雄技能</b>使你所有的随从获得<b>圣盾</b>。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
	}
}