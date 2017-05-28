using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCurrency : Inventory, IInventory
{
    public TextAsset ItemsDB { get; set; }
    public List<Item> Items { get; set; }
    

    public void Init()
    {
        base.Init(Items, CellPrefab, transform);
    }

    private void Awake()
    {
        ItemsDB = Resources.Load<TextAsset>("Shop/GameCurrency");
        Items = JsonConvert.DeserializeObject<List<Item>>(ItemsDB.text);

        Init();
    }
}
