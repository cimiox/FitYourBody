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
        if (PlayerAttributes.RemoveMoney(cell.Properties.Cost) && (BoostDatabase.Boosts.Count < BoostsHandler.MaxBoosts))
        {
            OnBought?.Invoke(this);
            return true;
        }

        //TODO: message
        return false;
    }
}
