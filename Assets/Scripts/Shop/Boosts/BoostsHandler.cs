using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostsHandler : MonoBehaviour
{
    private static BoostsHandler instance;
    public static BoostsHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoostsHandler>();

                if (instance == null)
                {
                    GameObject container = new GameObject("BoostsHandler");
                    instance = container.AddComponent<BoostsHandler>();
                }
            }

            return instance;
        }
    }

    public static readonly int MaxBoosts = 2;

    private void Start()
    {
        BoostDatabase.Boosts.CollectionChanged += Boosts_CollectionChanged;
    }

    private void Boosts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (var item in e.NewItems)
            {
                (item as Boost).TimerCoroutine = StartCoroutine((item as Boost).TimerEnumerator());
            }
    }

    public void BoostsHandler_OnBought(Item item)
    {
        if (item is SportNutritionItem)
            BoostDatabase.Boosts.Add(new SportNutritionBoost(new Boost.Timer(DateTime.Now + TimeSpan.FromSeconds((item as SportNutritionItem).Time))));
        else if (item is SportingGoodsItem)
            BoostDatabase.Boosts.Add(new SportingGoodsBoost(new Boost.Timer(DateTime.MaxValue)));
    }
}
