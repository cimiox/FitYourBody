using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public GameObject PlayerFront;
    public GameObject PlayerBack;

    private void Start()
    {
        StartCoroutine(WaitMusclesSetted());
    }

    private IEnumerator WaitMusclesSetted()
    {
        yield return new WaitUntil(() => MuscleController.Muscles.Count > 0);
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
