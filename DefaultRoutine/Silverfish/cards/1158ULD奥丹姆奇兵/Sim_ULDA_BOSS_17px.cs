namespace HREngine.Bots
{
	class Sim_ULDA_BOSS_17px : SimTemplate //* 传播诅咒 Spreading the Curse
//[x]<b>Hero Power</b>Summon a Leper Gnomeand give it <b>Rush</b>.
//<b>英雄技能</b>召唤一个麻风侏儒，并使其获得<b>突袭</b>。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}