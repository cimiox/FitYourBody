using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public static List<Boost> Boosts { get; set; } = new List<Boost>();

    public static readonly int MaxBoosts = 2;

    private void Start()
    {
        Load();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Boosts.fyb"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Open(Application.persistentDataPath + "/Boosts.fyb", FileMode.Open))
            {
                Boosts = (List<Boost>)bf.Deserialize(file);
            }

            foreach (var item in Boosts)
            {
                GameObject obj = Instantiate(Instance.Boost, Instance.transform);
                obj.GetComponentInChildren<Image>().sprite = item.Properties.Sprite();

                item.OnTickHandler += obj.GetComponentInChildren<TicksHandler>().CallTicks;
            }
        }
    }

    public static void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(Application.persistentDataPath + "/Boosts.fyb"))
            {
                bf.Serialize(file, Boosts);
            }
        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
        
    }

    public void BoughtHandler_OnBought(Item item)
    {
        GameObject obj = Instantiate(Boost, transform);
        obj.GetComponentInChildren<Image>().sprite = item.Sprite();

        Boost inst = new Boost();
        inst.Properties = item;
        inst.EndTime = DateTime.Now + TimeSpan.FromSeconds((item as SportNutritionItem).Time);
        //inst.OnTickHandler += obj.GetComponentInChildren<TicksHandler>().CallTicks;
        inst.NowTime = inst.EndTime - DateTime.Now;

        Boosts.Add(inst);
    }
}
