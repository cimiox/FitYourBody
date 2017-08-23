using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
        get
        {
            return Resources.Load<GameObject>("Shop/Boost");
        }
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
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(Application.persistentDataPath + "/Boosts.fyb"))
        {
            bf.Serialize(file, Boosts);
        }
    }

    public void BoughtHandler_OnBought(Item item)
    {
        Boost boost = Instantiate(Boost, transform).GetComponent<Boost>();
        boost.Properties = item;
        boost.StartTime = DateTime.Now;
        
        boost.EndTime = DateTime.Now + TimeSpan.FromSeconds((item as SportNutritionItem).Time);
        boost.CallTicks();
        //TODO: boost.Image
        Boosts.Add(boost);
    }
}
