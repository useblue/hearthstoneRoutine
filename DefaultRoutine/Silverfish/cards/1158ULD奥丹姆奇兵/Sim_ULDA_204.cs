namespace HREngine.Bots
{
	class Sim_ULDA_204 : SimTemplate //* 雷诺的魔法火把 Reno's Magical Torch
//Deal $@ damage.<b>Combo:</b> Shuffle a copy of this card into your deck that deals 2 more damage.
//造成$@点伤害。<b>连击：</b>将这张牌的一张复制洗入你的牌库，其造成的伤害增加2点。 
	{
		
		



        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
	}
}