using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuscleSystem : MonoBehaviour
{
    public delegate void ChangingClicks(float count);
    public static event ChangingClicks ChangeClicks;

    public static GameObject ZoomableGO { get; set; }
    public static float Multiplier { get; set; }

    private int muscleLevel;
    public int MuscleLevel
    {
        get { return PlayerPrefs.GetInt("MuscleLevel", 1);}
        set 
        {
            muscleLevel = value;
            PlayerPrefs.SetInt("MuscleLevel", muscleLevel);
        }
    }
    
    private float clicks;
    public float Clicks
    {
        get { return clicks; }
        set
        {
            if (ChangeClicks != null)
                ChangeClicks(value - clicks);

            clicks = value;
        }
    }

    public bool isZoom { get; set; }

    private void OnMouseDown()
    {
        ZoomableGO = gameObject;

        if (!isZoom)
        {
            Init();
            return;
        }

        Clicks += Convert.ToInt32(1 * Multiplier);
    }

    private void Init()
    {
        ZoomSystem.Zoom(ZoomableGO);
        isZoom = true;
        Multiplier = 1;
    }
}
