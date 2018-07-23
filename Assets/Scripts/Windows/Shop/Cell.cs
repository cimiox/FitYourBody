using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private TextMeshProUGUI multiplier;
    public TextMeshProUGUI Multiplier { get { return multiplier; } } 

    public Text Name { get; set; }

    public Text Description { get; set; }

    private Item properties;
    public Item Properties
    {
        get { return properties; }
        set
        {
            if (value != null)
            {
                properties = value;
                Intialize();
            }
        }
    }


    public void Intialize()
    {
        Multiplier.text = $"X{(Properties as SportNutritionItem).Multiplier.ToString()}";
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
