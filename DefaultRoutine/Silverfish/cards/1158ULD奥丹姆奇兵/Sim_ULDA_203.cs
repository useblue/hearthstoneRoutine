namespace HREngine.Bots
{
	class Sim_ULDA_203 : SimTemplate //* 雷诺的幸运帽 Reno's Lucky Hat
//Give a minion +2/+2 and <b>Spell Damage +2</b>.
//使一个随从获得+2/+2以及<b>法术伤害+2</b>。 
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