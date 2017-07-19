using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public GameObject player;
    public Text clicksText;
    public static Slider experience;

    void Awake()
    {
        experience = GameObject.Find("Experience").GetComponent<Slider>();
    }
    private void Start()
    {
        for (int i = 0; i < Muscle.Muscles.Count; i++)
        {
            Muscle.Muscles[i].Muscle.ChangeClicks += player.GetComponent<PlayerAttributes>().Muscle_ChangeClicks;
        }
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
