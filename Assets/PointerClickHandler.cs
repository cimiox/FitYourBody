using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    private static Muscle Muscle { get; set; }
    private static GameObject ClickHandler { get; set; }

    private void Start()
    {
        ClickHandler = gameObject;
        ClickHandler.SetActive(false);
    }

    public static void Intialize(Muscle muscle)
    {
        ClickHandler.SetActive(true);
        Muscle = muscle;
    }

    public static void Close()
    {
        ClickHandler.SetActive(false);
        Muscle = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Muscle.Click();
    }
}
