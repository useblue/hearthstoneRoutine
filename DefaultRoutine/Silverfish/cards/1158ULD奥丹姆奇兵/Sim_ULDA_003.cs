namespace HREngine.Bots
{
	class Sim_ULDA_003 : SimTemplate //* 埃达尔拉 Addarah
//<b>Battlecry:</b> Shuffle all enemy minions into your deck.
//<b>战吼：</b>将所有敌方随从洗入你的牌库。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_HERO_TARGET),
            };
        }
	}
}