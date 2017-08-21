using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private GameObject boost;
    public GameObject Boost
    {
        get
        {
            return boost = Resources.Load<GameObject>("Shop/Boost");
        }
    }

    public Stack<GameObject> Boosts { get; set; } = new Stack<GameObject>();

    private static readonly int MaxBoosts = 2;

    void Start()
    {

    }

    public void BoughtHandler_OnBought(Item item)
    {
        //TODO: Add stack
        Boosts.Push(Boost);
    }
}
