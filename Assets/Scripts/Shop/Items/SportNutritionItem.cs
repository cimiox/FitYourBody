using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionItem : Item
{
    public double Multiplier { get; set; }
    public float Time { get; set; }

    public SportNutritionItem(int id, string name, double multiplier, float time, double cost, int levelNeed)
        : base(id, name, cost, levelNeed)
    {
        Multiplier = multiplier;
        Time = time; 
    }
}
