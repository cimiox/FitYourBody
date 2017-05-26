using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory Instance = null;

    public static TextAsset ItemsDB { get; set; }
    public static List<Item> Items { get; set; }
    public static GameObject CellPrefab { get; set; }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyObject(this);

        Init();
    }


    public static void Init()
    {
        ItemsDB = Resources.Load<TextAsset>("Shop/itemsDB");
        Items = JsonConvert.DeserializeObject<List<Item>>(ItemsDB.text);
        CellPrefab = Resources.Load<GameObject>("Shop/Cell");

        for (int i = 0; i < Items.Count; i++)
        {
            GameObject cell = Instantiate(CellPrefab, Instance.transform);
            cell.GetComponent<Cell>().Name.text = Items[i].Name;
            cell.GetComponent<Cell>().Sprite.sprite = Items[i].Sprite;
            cell.GetComponent<Cell>().Description.text = Items[i].Multiplier.ToString();
            cell.GetComponent<Cell>().BuyBtn.GetComponentInChildren<Text>().text = string.Format("Buy({0})", Items[i].Cost);
        }
    }
}
