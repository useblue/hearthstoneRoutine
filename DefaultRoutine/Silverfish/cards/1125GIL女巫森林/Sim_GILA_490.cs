namespace HREngine.Bots
{
	class Sim_GILA_490 : SimTemplate //* 搜寻猎物 On the Hunt
//Add to your deck:'{0}''{1}''{2}'
//加入你的牌库：“{0}”“{1}”“{2}” 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}