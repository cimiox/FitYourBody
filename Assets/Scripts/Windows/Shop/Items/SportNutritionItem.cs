using UnityEngine;

public class SportNutritionItem : Item
{
    public float Multiplier { get; set; }
    public float Time { get; set; }

    public override Sprite Sprite
    {
        get
        {
            return Resources.Load<Sprite>("Shop/SportNutritionSprites/" + Image);
        }
    }

    public SportNutritionItem(int id, string name, float multiplier, float time, double cost, string spriteName, int levelNeed)
        : base(id, name, cost, spriteName, levelNeed)
    {
        Multiplier = multiplier;
        Time = time;
    }

    public override bool Buy(Cell cell, IShop shop)
    {
        if ((BoostDatabase.Boosts.Count < BoostsHandler.MaxBoosts) && PlayerAttributes.RemoveMoney(cell.Properties.Cost))
        {
            Bought();
            return true;
        }

        return false;
    }
}
