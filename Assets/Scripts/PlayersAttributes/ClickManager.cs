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
        for (int i = 0; i < PlayerAttributes.Muscles.Count; i++)
        {
            PlayerAttributes.Muscles[i].Muscle.Properties.OnClicksChanging += player.GetComponent<PlayerAttributes>().Muscle_ChangeClicks;
        }
    }
    
    public void BackToPlayer()
    {
        ZoomSystem.Detach();

        Camera.main.orthographicSize = 5f;
        Camera.main.transform.position = Vector3.zero;
    }

    
}
