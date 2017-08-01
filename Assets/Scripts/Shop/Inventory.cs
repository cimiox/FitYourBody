using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Inventory : MonoBehaviour
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

    protected virtual void Inititalize(IInventory type, Transform thisGO)
    {
        type.ItemsDB = Resources.Load<TextAsset>(type.Path);
        type.Items = GetItemsJson(type.ItemsDB);

        for (int i = 0; i < type.Items.Count; i++)
        {
            type.Items[i].IsUnlock = PlayerAttributes.Level <= type.Items[i].Level ? true : false;

            Cell cell = Instantiate(CellPrefab, thisGO).GetComponent<Cell>();
            cell.Name.text = type.Items[i].Name;
            cell.Sprite.sprite = type.Items[i].Sprite;
            cell.Description.text = type.Items[i].Multiplier.ToString();
            cell.Properties = type.Items[i];

            cell.BuyBtn.enabled = type.Items[i].IsUnlock ? true : false;
            cell.BuyBtn.GetComponentInChildren<Text>().text = string.Format("Buy({0})", type.Items[i].Cost);

            type.Cells.Add(cell);
        }
    }

    protected int GetNewItems(int level, IInventory inventory)
    {
        int countNotifications = 0;
        for (int i = 0; i < inventory.Cells.Count; i++)
        {
            if (level <= inventory.Cells[i].Properties.Level)
            {
                inventory.Cells[i].BuyBtn.enabled = true;
                countNotifications++;
            }
        }

        return countNotifications;
    }

    private List<Item> GetItemsJson(TextAsset json)
    {
        return JsonConvert.DeserializeObject<List<Item>>(json.text);
    }
}
