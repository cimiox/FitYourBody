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

        shop.Items = new List<Item>();
        shop.Items.AddRange(SetParametres(GetItemsJson<T>(shop.ItemsDB), shop, parent));
    }

    private List<T> SetParametres<T>(List<T> items, IShop shop, Transform parent) where T : Item
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].IsUnlock = PlayerAttributes.Level <= items[i].Level ? true : false;

            shop.Cells.Add(CreateCell(items[i], shop, parent));
        }
        return items;
    }

    protected virtual Cell CreateCell(Item item, IShop shop, Transform parent)
    {
        Cell cell = Instantiate(CellPrefab, parent).GetComponent<Cell>();
        cell.Name.text = item.Name;
        cell.Sprite.sprite = item.Sprite;
        //cell.Description.text = type.Items[i].Multiplier.ToString();
        cell.Properties = item;

        cell.BuyBtn.interactable = item.IsUnlock ? true : false;
        cell.BuyBtn.onClick.AddListener(() => item.Buy(shop));
        cell.BuyBtn.GetComponentInChildren<Text>().text = string.Format("Buy({0})", item.Cost);

        return cell;
    }

    protected int GetNewItems(int level, IShop Shop)
    {
        int countNotifications = 0;
        for (int i = 0; i < Shop.Cells.Count; i++)
        {
            if (level <= Shop.Cells[i].Properties.Level)
            {
                Shop.Cells[i].BuyBtn.enabled = true;
                countNotifications++;
            }
        }

        return countNotifications;
    }

    private List<T> GetItemsJson<T>(TextAsset json) where T : Item
    {
        return JsonConvert.DeserializeObject<List<T>>(json.text);
    }

    protected void DeactivateAllShops()
    {
        for (int i = 0; i < Shops.Count; i++)
        {
            Shops[i].gameObject.SetActive(false);
        }
    }
}
