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
        private set { cellPrefab = value; }
    }

    protected void Init(List<Item> items, GameObject prefab, Transform thisGO)
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject cell = Instantiate(prefab, thisGO);
            var cellComponent = cell.GetComponent<Cell>();
            cellComponent.Name.text = items[i].Name;
            cellComponent.Sprite.sprite = items[i].Sprite;
            cellComponent.Description.text = items[i].Multiplier.ToString();
            cellComponent.BuyBtn.GetComponentInChildren<Text>().text = string.Format("Buy({0})", items[i].Cost);
            cellComponent.Properties = items[i];
        }
    }
}
