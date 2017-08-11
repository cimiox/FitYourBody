using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportingGoodsItem : Item
{
    public string Description { get; set; }

    public SportingGoodsItem(int id, string name, string description, double cost, int levelNeed)
        : base(id, name, cost, levelNeed)
    {
        Description = description;
    }
}
