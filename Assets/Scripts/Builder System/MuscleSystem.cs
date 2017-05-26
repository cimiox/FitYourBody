using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleSystem : MonoBehaviour
{
    public delegate void ChangingText();
    public static event ChangingText ChangeText;

    public static GameObject ZoomableGO { get; set; }
    public static float Multiplier { get; set; }

    private int clicks;
    public int Clicks
    {
        get { return clicks; }
        set
        {
            clicks = value;
            ChangeText();
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
        ZoomSystem.Zoom(ZoomableGO = gameObject);
        isZoom = true;
        Multiplier = 1;
    }
}
