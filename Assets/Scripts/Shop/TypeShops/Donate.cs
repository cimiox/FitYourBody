﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : Inventory, IInventory
{
	public TextAsset ItemsDB { get; set; }
    public List<Item> Items { get; set; }
    public string Path { get; set; }
	public List<Cell> Cells { get; set; }
	public static int CountNotifications { get; set; }	

	protected override void Inititalize(IInventory type, Transform thisGO)
	{
		Cells = new List<Cell>();
		Path = "Shop/DonateShop";
		
		base.Inititalize(type, thisGO);

		PlayerAttributes.OnLevelChanged += LevelChanged_OnLevelChanged;
	}

	private void LevelChanged_OnLevelChanged()
	{
		CountNotifications = base.GetNewItems(PlayerAttributes.Level, this);
	}

	private void Awake()
	{
		Inititalize(this, transform);
	}
}
