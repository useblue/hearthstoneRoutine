namespace HREngine.Bots
{
	class Sim_ULD_715 : SimTemplate //* 疯狂之灾祸 Plague of Madness
	{
		//Each player equipsa 2/2 Knife with <b>Poisonous</b>.
		//每个玩家装备一把2/2并具有<b>剧毒</b>的刀。
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ULD_715t);

			p.equipWeapon(weapon, ownplay);
			p.equipWeapon(weapon, !ownplay);
		}

	}
}