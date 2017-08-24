[System.Serializable]
public class SportNutritionItem : Item
{
    public override event Bought OnBought;
    public double Multiplier { get; set; }
    public float Time { get; set; }

    public SportNutritionItem(int id, string name, double multiplier, float time, double cost, int levelNeed)
        : base(id, name, cost, levelNeed)
    {
        Multiplier = multiplier;
        Time = time;
    }

    public override bool Buy(Cell cell, IShop shop)
    {
        if (base.Buy(cell, shop))
        {
            OnBought?.Invoke(this);
            return true;
        }
        return false;
    }
}
