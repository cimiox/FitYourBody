﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Cost { get; set; }
    public int Level { get; set; }
    public bool IsUnlock { get; set; }
    public bool IsBuyed { get; set; }
    public Sprite Sprite { get { return Resources.Load<Sprite>(Image); } }

    public Item(int id, string name, double cost, int levelNeed)
    {
        ID = id;
        Name = name;
        Cost = cost;
        Level = levelNeed;
    }

    public bool Buy(Cell cell, IShop shop)
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