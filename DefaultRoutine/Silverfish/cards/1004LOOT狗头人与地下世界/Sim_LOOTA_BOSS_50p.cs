namespace HREngine.Bots
{
	class Sim_LOOTA_BOSS_50p : SimTemplate //* 蘑菇，蘑菇 Mushroom, Mushroom
//<b>Hero Power</b>Craft a custom Mushroom Potion.
//<b>英雄技能</b>制造一瓶自定义的蘑菇药水。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1),
            };
        }
	}
}