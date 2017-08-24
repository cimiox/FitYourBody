using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicksHandler : MonoBehaviour
{
    public Text Text { get; set; }

    private void Awake()
    {
        Text = GetComponent<Text>();
    }

    public void CallTicks(Boost boost)
    {
        StartCoroutine(Ticks(boost));
    }

    private IEnumerator Ticks(Boost boost)
    {
        while (true)
        {
            boost.NowTime =  boost.EndTime - DateTime.Now;
            Text.text = boost.NowTime.ToString("m\\:f");
            yield return new WaitForSeconds(1f);
        }
    }

}
