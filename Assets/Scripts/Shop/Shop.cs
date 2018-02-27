using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Shop : MonoBehaviour
{
    private GameObject cellPrefab;
    public GameObject CellPrefab
    {
        get
        {
            if (cellPrefab == null)
                return cellPrefab = Resources.Load<GameObject>("Shop/Cell");
            return cellPrefab;
        }
    }

    public static List<Shop> Shops { get; set; } = new List<Shop>();

    protected virtual void Inititalize<T>(IShop shop, Transform parent) where T : Item
    {
        shop.ItemsDB = Resources.Load<TextAsset>(shop.Path);

        shop.Cells = new List<Cell>();
        shop.Cells.AddRange(SetParametres(GetItemsJson<T>(shop.ItemsDB), shop, parent));
    }

    private List<Cell> SetParametres<T>(List<T> items, IShop shop, Transform parent) where T : Item
    {
        List<Cell> cells = new List<Cell>();
        for (int i = 0; i < items.Count; i++)
        {
            items[i].IsUnlock = PlayerAttributes.PlayerProperties.Level <= items[i].Level ? true : false;
            
            cells.Add(CreateCell(items[i], shop, parent));
            cells[i].Properties = items[i];
        }
        return cells;
    }

    protected virtual Cell CreateCell<T>(T item, IShop shop, Transform parent) where T : Item
    {
        Cell cell = Instantiate(CellPrefab, parent).GetComponent<Cell>();

        //TODO: Shop attributes

        //cell.Name.text = item.Name;
        //cell.Description.text = type.Items[i].Multiplier.ToString();
        //cell.Properties = item;

        cell.Sprite.sprite = item.Sprite();
        item.OnBought += BoostsHandler.Instance.BoostsHandler_OnBought;
        cell.BuyBtn.interactable = item.IsUnlock ? true : false;
        cell.BuyBtn.onClick.AddListener(() => item.Buy(cell, shop));
        cell.BuyBtn.GetComponentInChildren<Text>().text = string.Format("Buy({0})", item.Cost);
        return cell;
    }

    protected int GetNewItems(int level, IShop Shop)
    {
        int countNotifications = 0;

        foreach (var item in Shop.Cells)
        {
            if (level <= item.Properties.Level)
            {
                item.BuyBtn.enabled = true;
                countNotifications++;
            }
        }

        return countNotifications;
    }

    private List<T> GetItemsJson<T>(TextAsset json) where T : Item
    {
        return JsonConvert.DeserializeObject<List<T>>(json.text);
    }

    public void DeactivateShop()
    {
        gameObject.SetActive(false);
    }
}
