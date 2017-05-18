using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour, IPointerClickHandler
{
    public Text clicks;
    public Slider experience;

    public float Count { get; set; }
    public int Level { get; set; }

    private static readonly float EXP = 150;

    public void OnPointerClick(PointerEventData eventData)
    {
        clicks.text = string.Format("Clicks: {0}\nLevel: {1}", ++Count, Level);

        experience.maxValue = GetExp(Level);
        experience.value = Count;

        if (Count >= GetExp(Level))
        {
            Count = 0;
            ++Level;
        }
    }

    private void Init()
    {
        Level = 1;
    }

    private void Update()
    {

    }

    private static float GetExp(float level)
    {
        if (level <= 1)
            return EXP;

        return EXP * level + GetExp(level - 1);
    }
}
