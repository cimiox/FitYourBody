using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuscleSystem : MonoBehaviour
{
    public delegate void ChangingClicks(Slider slider);
    public static event ChangingClicks ChangeClicks;

    public static GameObject ZoomableGO { get; set; }
    public static float Multiplier { get; set; }

    private float clicks;
    public float Clicks
    {
        get { return clicks; }
        set
        {
            clicks = value;
            if (ChangeClicks != null)
                ChangeClicks(ClickManager.experience);
        }
    }
    public bool isZoom { get; set; }

    private void OnMouseDown()
    {
        if (!isZoom)
        {
            Init();
            return;
        }

        Clicks += Convert.ToInt32(1 * Multiplier);
    }

    private void Init()
    {
        //ZoomSystem.Zoom(ZoomableGO);
        isZoom = true;
        Multiplier = 1;
    }
}
