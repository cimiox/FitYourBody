using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionBoost : Boost
{
    public delegate void EndBoost();
    public event EndBoost OnEndBoost;

    public SportNutritionBoost(Timer timer, Item properties) : base(timer, properties)
    {
        PlayerAttributes.PlayerProperties.Multiplier += (Properties as SportNutritionItem).Multiplier;

        OnEndBoost += () => PlayerAttributes.PlayerProperties.Multiplier -= (Properties as SportNutritionItem).Multiplier;
    }

    public override IEnumerator TimerEnumerator()
    {
        BoostTimer.NowTime = DateTime.Now;
        while (BoostTimer.NowTime < BoostTimer.EndTime)
        {
            BoostTimer.NowTime = DateTime.Now;
            yield return new WaitForSeconds(1f);
        }

        yield return null;

        OnEndBoost?.Invoke();
    }
}
