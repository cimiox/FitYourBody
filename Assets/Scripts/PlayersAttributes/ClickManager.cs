using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public Text clicksText;
    public static Slider experience;

    private void Init()
    {
        PlayerAttributes.Experience.Level = 1;
    }
    void Awake()
    {
        experience = GameObject.Find("Experience").GetComponent<Slider>();
    }
    private void Start()
    {
        MuscleSystem.ChangeClicks += PlayerAttributes.Experience.MuscleSystem_ChangeClicks;
        PlayerAttributes.Init();
        StartCoroutine(PlayerAttributes.Experience.UpLevelAnimation(experience, 100, 1));
    }
    
    private static bool isNextLevel()
    {
        return true;
        //return AllClicks(GameObject.Find("Img")) >= PlayerAttributes.experience.GetExp(PlayerAttributes.experience.Level);
    }

    

    public void BackToPlayer()
    {
        //var sumClicks = AllClicks(GameObject.Find("Player"));
        //clicksText.text = string.Format("All Clicks: {0}\nLevel: {1}", sumClicks, PlayerAttributes.experience.Level);

        ZoomSystem.Detach();

        Camera.main.orthographicSize = 5f;
        Camera.main.transform.position = Vector3.zero;
    }

    
}
