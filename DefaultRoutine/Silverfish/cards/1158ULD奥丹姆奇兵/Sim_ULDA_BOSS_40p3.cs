namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_40p3 : SimTemplate //* 蔑视之手 Hand of Defiance
//[x]<b>Hero Power</b>Swap the Attack andHealth of a minion.
//<b>英雄技能</b>交换一个随从的攻击力和生命值。 
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