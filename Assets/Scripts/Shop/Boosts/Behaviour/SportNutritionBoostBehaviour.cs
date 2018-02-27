using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportNutritionBoostBehaviour : BoostBehaviour
{
    protected override void Start()
    {
        base.Start();

        Boost.BoostTimer.PropertyChanged += BoostTimer_PropertyChanged;
    }

    private void BoostTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        TimerText.text = (Boost.BoostTimer.EndTime - Boost.BoostTimer.NowTime).Seconds.ToString();
    }
}
