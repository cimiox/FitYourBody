using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicksHandler : MonoBehaviour
{
    public Text Text { get; set; }
    public Boost Boost { get; set; }

    private void Initialize(Boost boost)
    {
        Text = GetComponent<Text>();
        Boost = boost;
        Boost.BoostTimer.PropertyChanged += BoostTimer_PropertyChanged;
    }

    private void BoostTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.Equals("NowTime"))
            Text.text = (Boost.BoostTimer.EndTime - Boost.BoostTimer.NowTime).Seconds.ToString();
    }
}
