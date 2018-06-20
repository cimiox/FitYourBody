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

    public List<Boost> CreatedBoosts { get; set; } = new List<Boost>();

    private void Start()
    {
        BoostDatabase.Boosts.CollectionChanged += Boosts_CollectionChanged;
        BoostDatabase.Load();

        OnAddNewItems(BoostDatabase.Boosts);
    }

    private void Boosts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            OnAddNewItems(e.NewItems);
    }

    private void CreateBoosts()
    {
        var boostsFactory = new SportNutritionBoostsFactory();
        var sportNutritionBoosts = BoostDatabase.Boosts.Where(x => x is SportNutritionBoost).Select(x => x as SportNutritionBoost).ToList();

        for (int i = 0; i < sportNutritionBoosts.Count; i++)
        {
            if (!CreatedBoosts.Contains(sportNutritionBoosts[i]) && !sportNutritionBoosts[i].IsEnd)
            {
                var go = boostsFactory.CreateBoost(sportNutritionBoosts[i], StackForBoost);

                sportNutritionBoosts[i].OnEndBoost += () => { CreatedBoosts.Remove(sportNutritionBoosts[i]); };

                CreatedBoosts.Add(sportNutritionBoosts[i]);
            }
        }
    }

    private void OnAddNewItems(IList newItems)
    {
        foreach (var item in newItems)
        {
            (item as Boost).TimerCoroutine = StartCoroutine((item as Boost).TimerEnumerator());
        }

        CreateBoosts();
    }

    public void BoostsHandler_OnBought(Item item)
    {
        if (item is SportNutritionItem)
            BoostDatabase.Boosts.Add(new SportNutritionBoost(new Boost.Timer(DateTime.Now + TimeSpan.FromSeconds((item as SportNutritionItem).Time)), item));
        else if (item is SportingGoodsItem)
            BoostDatabase.Boosts.Add(new SportingGoodsBoost(new Boost.Timer(DateTime.MaxValue), item));

        CreateBoosts();

        BoostDatabase.Save();
    }
}
