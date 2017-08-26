public class SportNutritionItem : Item
{
    public override event Bought OnBought;
    public float Multiplier { get; set; }
    public float Time { get; set; }

    public SportNutritionItem(int id, string name, float multiplier, float time, double cost, int levelNeed)
        : base(id, name, cost, levelNeed)
    {
        Multiplier = multiplier;
        Time = time;
    }

    public override bool Buy(Cell cell, IShop shop)
    {
        if (BoostDatabase.Boosts.Count < BoughtHandler.MaxBoosts)
        {
            if (base.Buy(cell, shop))
            {
                OnBought?.Invoke(this);
                return true;
            }

            return false;
        }
        else
        {
            //TODO: message
            return false;
        }
    }
}
