using Newtonsoft.Json;
using UnityEngine;

public class Item
{
    private int levelNeed;

    public delegate void Bought(Item item);
    public virtual event Bought OnBought;

    public int ID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Cost { get; set; }
    public int Level { get; set; }
    public bool IsUnlock { get { return PlayerAttributes.PlayerProperties.Level <= Level ? false : true; } }
    public bool IsBuyed { get; set; }

    [JsonIgnore]
    public virtual Sprite Sprite
    {
        get { return Resources.Load<Sprite>(Image); }
    }

    public Item()
    {

    }

    public Item(int id, string name, double cost, string spriteName, int levelNeed)
    {
        ID = id;
        Name = name;
        Cost = cost;
        Image = spriteName;
        Level = levelNeed;
    }

    public virtual bool Buy(Cell cell, IShop shop)
    {
        if (PlayerAttributes.RemoveMoney(cell.Properties.Cost))
        {
            cell.Remove(shop.Cells);
            return true;
        }
        else
            return false;
    }
}
