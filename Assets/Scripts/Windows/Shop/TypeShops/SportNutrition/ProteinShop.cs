using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinShop : SportNutrition, IShop 
{
	public TextAsset ItemsDB { get; set; }
    public List<Item> Items { get; set; }
    public string Path { get; set; }
	public List<Cell> Cells { get; set; }
	public static int CountNotifications { get; set; }	

	protected override void Inititalize<T>(IShop type, Transform thisGO)
	{
        Shops.Add(this);

        Cells = new List<Cell>();

        Path = "Shop/ProteinShop";

		base.Inititalize<T>(type, thisGO);

		PlayerAttributes.Properties.OnLevelChanged += LevelChanged_OnLevelChanged;
	}

    protected override Cell CreateCell<T>(T item, IShop shop, Transform parent)
    {
        return base.CreateCell(item, shop, parent);
    }

    private void LevelChanged_OnLevelChanged()
	{
		CountNotifications = GetNewItems(PlayerAttributes.PlayerProperties.Level, this);
	}

	protected override void OnEnable()
	{
		Inititalize<SportNutritionItem>(this, transform);

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        foreach (var item in Cells)
        {
            Destroy(item.gameObject);
        }

        base.OnDisable();
    }

    public void Activate(string animation)
    {
        gameObject.SetActive(true);

        //TODO: Animation
    }
}
