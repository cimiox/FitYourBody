using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Multiplier { get; set; }
    public float Time { get; set; }
    public double Cost { get; set; }
    public int Level { get; set; }
    public bool IsUnlock { get; set; }
    public Sprite Sprite { get { return Resources.Load<Sprite>(Image); } }

    public Item(int id, string name, double multiplier, float time, double cost, int levelNeed)
    {
        ID = id;
        Name = name;
        Multiplier = multiplier;
        Time = time;
        Cost = cost;
        Level = levelNeed;
    }
}
