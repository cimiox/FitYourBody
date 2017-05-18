using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour, IPointerClickHandler
{
    public Text clicksText;
    public Slider experience;

    public static float Count { get; set; }
    public int Level { get; set; }
    private static readonly float EXP = 150;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ZoomSystem.isClick)
            return;

        clicksText.text = string.Format("Clicks: {0}\nLevel: {1}", ++Count, Level);

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

    private void Start()
    {
        MuscleSystem.ChangeText += MuscleSystem_ChangeText;
    }

    private void MuscleSystem_ChangeText()
    {
        clicksText.text = string.Format("Clicks: {0}\nLevel: {1}", MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks, Level);
    }

    private static float GetExp(float level)
    {
        if (level <= 1)
            return EXP;

        return EXP * level + GetExp(level - 1);
    }
}
