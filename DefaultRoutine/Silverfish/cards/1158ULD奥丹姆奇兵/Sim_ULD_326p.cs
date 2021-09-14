namespace HREngine.Bots
{
	class Sim_ULD_326p : SimTemplate //* 远古刀锋 Ancient Blades
	{
		//[x]<b>Hero Power</b>Equip a 3/2 Blade with <b>Immune</b> while attacking.
		//<b>英雄技能</b>装备一把3/2的战刃，在攻击时具有<b>免疫</b>。
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
		{
			CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ULD_326t);
			p.equipWeapon(weapon, ownplay);
		}

	}
}