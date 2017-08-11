using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : Shop, IShop
{
    public TextAsset ItemsDB { get; set; }
    public List<Item> Items { get; set; }
    public string Path { get; set; }
    public List<Cell> Cells { get; set; }
    public static int CountNotifications { get; set; }

    protected override void Inititalize<T>(IShop type, Transform thisGO)
    {
        Shops.Add(this);

        Cells = new List<Cell>();

        Path = "Shop/DonateShop";

        base.Inititalize<T>(type, thisGO);

        PlayerAttributes.OnLevelChanged += LevelChanged_OnLevelChanged;
    }

    protected override Cell CreateCell(Item item, IShop shop, Transform parent)
    {
        var cell = base.CreateCell(item, shop, parent);

        return cell;
    }

    private void LevelChanged_OnLevelChanged()
    {
        CountNotifications = GetNewItems(PlayerAttributes.Level, this);
    }

    private void Awake()
    {
        Inititalize<DonateItem>(this, transform);
    }

    public void Activate(string animation)
    {
        DeactivateAllShops();

        gameObject.SetActive(true);

        //TODO: Animation
    }
}
