using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoughtHandler : MonoBehaviour
{
    private static BoughtHandler instance;
    public static BoughtHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoughtHandler>();

                if (instance == null)
                {
                    GameObject container = new GameObject("BoughtHandler");
                    instance = container.AddComponent<BoughtHandler>();
                }
            }

            return instance;
        }
    }

    public GameObject Boost
    {
        get { return Resources.Load<GameObject>("Shop/Boost"); }
    }

    public static readonly int MaxBoosts = 2;

    private void Start()
    {
        BoostDatabase.Load();
        List<Boost> boostsToRemove = new List<Boost>();

        foreach (var item in BoostDatabase.Boosts)
        {
            item.isFirstCall = true;
            GameObject obj = Instantiate(Instance.Boost, Instance.transform);

            item.OnTickHandler += obj.GetComponentInChildren<TicksHandler>().CallTicks;
            item.NowTime = (item.EndTime - DateTime.Now).TotalSeconds;

            if (item.NowTime <= 0)
            {
                Destroy(obj);
                boostsToRemove.Add(item);
                continue;
            }

            obj.GetComponentInChildren<Image>().sprite = item.Properties.Sprite();
            obj.GetComponentInChildren<Text>().text = Math.Truncate(item.NowTime).ToString();
        }

        foreach (var item in boostsToRemove)
        {
            BoostDatabase.Boosts.Remove(item);
        }

        BoostDatabase.Save();
    }

    public void BoughtHandler_OnBought(Item item)
    {
        GameObject obj = Instantiate(Boost, transform);
        obj.GetComponentInChildren<Image>().sprite = item.Sprite();

        Boost inst = new Boost();
        inst.Properties = item;
        inst.EndTime = DateTime.Now + TimeSpan.FromSeconds((item as SportNutritionItem).Time);
        inst.OnTickHandler += obj.GetComponentInChildren<TicksHandler>().CallTicks;
        inst.NowTime = (inst.EndTime - DateTime.Now).TotalSeconds;
        BoostDatabase.Boosts.Add(inst);
    }
}
