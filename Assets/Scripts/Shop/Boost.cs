using System;
using UnityEngine;

public class Boost
{
    public delegate void TickHandler(Boost boost);
    public event TickHandler OnTickHandler;

    public bool isFirstCall = true;

    public Item Properties { get; set; }

    [SerializeField]
    private double nowTime;
    public double NowTime
    {
        get { return Math.Truncate(nowTime); }
        set
        {
            if (isFirstCall)
            {
                isFirstCall = false;
                OnTickHandler?.Invoke(this);
            }
            nowTime = value;
            BoostDatabase.Save();
        }
    }
    public DateTime EndTime { get; set; }

    public Boost()
    {
        if (Properties != null)
            PlayerAttributes.PlayerProperties.Multiplier += (Properties as SportNutritionItem).Multiplier;
    }

    ~Boost()
    {
        if (Properties != null)
            PlayerAttributes.PlayerProperties.Multiplier -= (Properties as SportNutritionItem).Multiplier;
    }
}
