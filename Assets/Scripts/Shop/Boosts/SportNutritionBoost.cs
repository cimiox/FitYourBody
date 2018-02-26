using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionBoost : Boost
{
    public delegate void EndBoost();
    public event EndBoost OnEndBoost;

    public override IEnumerator TimerEnumerator()
    {
        while (DateTime.Now >= BoostTimer.EndTime)
        {
            BoostTimer.NowTime = BoostTimer.EndTime - DateTime.Now;
            yield return new WaitForSeconds(1f);
        }

        OnEndBoost?.Invoke();
    }
}
