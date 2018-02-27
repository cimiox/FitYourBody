using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private Transform StackForBoost;

    private void Start()
    {
        BoostDatabase.Boosts.CollectionChanged += Boosts_CollectionChanged;

        BoostDatabase.Load();

        var boostFactory = new SportNutritionBoostsFactory();
        var sportNutritionBoosts = BoostDatabase.Boosts.Where(x => x is SportNutritionBoost).Select(x => x as SportNutritionBoost).ToList();

        for (int i = 0; i < sportNutritionBoosts.Count; i++)
        {
            if (sportNutritionBoosts[i].BoostTimer.EndTime < DateTime.Now)
            {
                boostFactory.CreateBoost(sportNutritionBoosts[i], StackForBoost);

                sportNutritionBoosts[i].OnEndBoost += () =>
                {
                    BoostDatabase.Boosts.Remove(sportNutritionBoosts[i]);
                    BoostDatabase.Save();
                };
            }
        }
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
