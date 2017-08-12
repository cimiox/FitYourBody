using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Text Name { get; set; }
    public Image Sprite { get; set; }
    public Text Description { get; set; }
    public Button BuyBtn { get; set; }
    public Item Properties { get; set; }

    private void Awake()
    {
        Name = gameObject.transform.GetChild(0).GetComponent<Text>();
        Sprite = gameObject.transform.GetChild(1).GetComponent<Image>();
        Description = gameObject.transform.GetChild(2).GetComponent<Text>();
        BuyBtn = gameObject.transform.GetChild(3).GetComponent<Button>();
    }

    public void Remove(List<Cell> cells)
    {
        cells.Remove(this);
        Destroy(gameObject);
    }
}
