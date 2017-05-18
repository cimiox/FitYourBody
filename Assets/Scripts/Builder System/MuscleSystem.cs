using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleSystem : MonoBehaviour
{
    public delegate void ChangingText();
    public static event ChangingText ChangeText;

    public static GameObject ZoomableGO { get; set; }

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
    public float Multiplier { get; set; }
    public bool isZoom { get; set; }

    private void OnMouseDown()
    {
        if (!isZoom)
        {
            ZoomSystem.Zoom(gameObject);
            isZoom = true;
            Init();
            return;
        }
        
        Clicks += 1;
    }

    private void Init()
    {
        ZoomableGO = gameObject;
    }
}
