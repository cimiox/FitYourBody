using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public double Multiplier { get; set; }
    public double Cost { get; set; }
    public int LevelNeed { get; set; }     
    public bool IsUnlock { get; set; }

    public Item(int id, string name, string sprite, double multiplier, double cost, int levelNeed)
    {
        ID = id;
        Name = name;
        Sprite = Resources.Load<Sprite>(sprite);
        Multiplier = multiplier;
        Cost = cost;
        LevelNeed = levelNeed;
    }
}
