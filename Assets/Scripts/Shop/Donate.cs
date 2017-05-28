using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : Inventory, IInventory
{
    public TextAsset ItemsDB { get; set; }
    public List<Item> Items { get; set; }

    public void Init()
    {
        base.Init(Items, CellPrefab, transform);
    }

    private void Awake()
    {
        ItemsDB = Resources.Load<TextAsset>("Shop/Donate");
        Items = JsonConvert.DeserializeObject<List<Item>>(ItemsDB.text);

        Init();
    }
}
