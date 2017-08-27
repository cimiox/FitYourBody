using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prizes;
    public GameObject[] Prizes
    {
        get { return prizes; }
        set { prizes = value; }
    }

    private void Start()
    {

    }

    private IEnumerator StartWheel()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
