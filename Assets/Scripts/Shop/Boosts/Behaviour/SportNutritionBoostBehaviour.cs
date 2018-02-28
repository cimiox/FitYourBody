using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionBoostBehaviour : BoostBehaviour
{
    protected override void Start()
    {
        base.Start();

        Boost.BoostTimer.PropertyChanged += BoostTimer_PropertyChanged;
        (Boost as SportNutritionBoost).OnEndBoost += () => Destroy(this);
        UpdateText();
    }

    private void BoostTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        TimerText.text = System.Math.Truncate((Boost.BoostTimer.EndTime - Boost.BoostTimer.NowTime).TotalSeconds).ToString();
    }
}
