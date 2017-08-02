using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public GameObject PlayerFront;
    public GameObject PlayerBack;

    private void Start()
    {
        PlayerBack.SetActive(false);
    }

    public void Rotate()
    {
        if (PlayerFront.activeInHierarchy)
        {
            PlayerFront.SetActive(false);
            PlayerBack.SetActive(true);
        }
        else
        {
            PlayerFront.SetActive(true);
            PlayerBack.SetActive(false);
        }
    }
}
