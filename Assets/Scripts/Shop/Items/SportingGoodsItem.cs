using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportingGoodsItem : Item
{
    public delegate void ChooseMuscle(Action actionAfterSelection);
    public static event ChooseMuscle OnChooseMuscle;

    public string Description { get; set; }

    public SportingGoodsItem(int id, string name, string description, double cost, int levelNeed)
        : base(id, name, cost, levelNeed)
    {
        Description = description;
    }

    public override bool Buy(Cell cell, IShop shop)
    {
        if (Cost <= PlayerAttributes.PlayerProperties.Money)
        {
            OnChooseMuscle?.Invoke(() => PlayerAttributes.RemoveMoney(Cost));
            return true;
        }

        return false;
    }
}
