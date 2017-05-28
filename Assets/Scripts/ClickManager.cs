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

    public static int Level { get; set; }
    public static int Money { get; set; }
    private static readonly float EXP = 150;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ZoomSystem.isClick)
            return;


        experience.maxValue = GetExp(Level);
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

    private static bool isNextLevel()
    {
        return AllClicks(GameObject.Find("Img")) >= GetExp(Level);
    }

    private static int AllClicks(GameObject parentGO)
    {
        int sum = 0;
        foreach (var item in parentGO.GetComponentsInChildren<MuscleSystem>())
        {
            sum += item.Clicks;
        }

        return sum;
    }

    public void BackToPlayer()
    {
        clicksText.text = string.Format("All Clicks: {0}\nLevel: {1}", AllClicks(GameObject.Find("Img")), Level);

        ZoomSystem.Detach();

        Camera.main.orthographicSize = 5f;
        Camera.main.transform.position = Vector3.zero;
    }
}
