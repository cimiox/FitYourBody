using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Image image;
    public Image Sprite { get { return image; } }

    [SerializeField]
    private Button buyButton;
    public Button BuyBtn { get { return buyButton; } }

    public Text Name { get; set; }

    public Text Description { get; set; }

    public Item Properties { get; set; }

    private void Awake()
    {
        Intialize();
    }

    public void Intialize()
    {
    }

    private void Properties_OnBought()
    {

    }

    public void Remove(List<Cell> cells)
    {
        cells.Remove(this);
        Destroy(gameObject);
    }
}
