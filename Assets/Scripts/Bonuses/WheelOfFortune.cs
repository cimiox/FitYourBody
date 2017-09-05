using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prizes;
    public GameObject[] Prizes
    {
        get { return prizes; }
        set { prizes = value; }
    }

    private GameObject palka;
    public GameObject Palka
    {
        get { return palka = palka == null ? Resources.Load<GameObject>("Palka") : palka; }
    }

    public GameObject Arrow { get; set; }

    public Quaternion StartPosition { get; set; } = Quaternion.identity;

    private void Start()
    {
        int angle = 360 / 7;
        for (int i = 1; i < 8; i++)
        {
            GameObject sprite = Instantiate(Palka, Vector3.zero, Quaternion.identity);
            sprite.transform.Rotate(0, 0, angle * i);
        }

        StartCoroutine(StartWheel());
    }

    public void OnMouseDown()
    {
        StartCoroutine(StartWheel());
    }

    private IEnumerator StartWheel()
    {
        float seconds = 15;
        float time = 0;
        float x = Random.Range(0.1f, 1.0f);
        while (time <= seconds)
        {
            time += Time.deltaTime;

            if (time <= seconds / 2)
                transform.Rotate(0, 0, time * Mathf.Log(x));
            else
                transform.Rotate(0, 0, (seconds - time) * Mathf.Log(Mathf.Exp(1), x));

            yield return null;
        }

        var prize = Prizes[0];

        for (int i = 0; i < Prizes.Length; i++)
        {
            for (int j = 0; j < Prizes.Length; j++)
            {
                if (Vector3.Distance(Prizes[i].transform.position, Arrow.transform.position)
                    < Vector3.Distance(Prizes[j].transform.position, Arrow.transform.position))
                {
                    prize = Prizes[j];
                }
            }
        }

        //TODO Show Prize
    }
}
