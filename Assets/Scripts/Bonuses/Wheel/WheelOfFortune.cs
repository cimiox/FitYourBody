using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField]
    private WheelOfFortunePrizesDB prizesDB;

    public GameObject[] Prizes { get; private set; }

    private GameObject separator;
    public GameObject Separator
    {
        get { return separator = separator == null ? Resources.Load<GameObject>("WheelOfFortune/Separator") : separator; }
    }

    [SerializeField]
    private GameObject objectForRotation;
    public GameObject ObjectForRotation
    {
        get { return objectForRotation; }
        set { objectForRotation = value; }
    }

    [SerializeField]
    private GameObject arrow;
    public GameObject Arrow
    {
        get { return arrow; }
        set { arrow = value; }
    }

    private float RotationTime { get; set; } = 5;
    private float DeltaTime { get; set; }
    private float WheelSpeed { get; set; }
    private WheelStates WheelState { get; set; } = WheelStates.Idle;

    public Quaternion StartPosition { get; set; } = Quaternion.identity;

    private void OnEnable()
    {
        IntializeWheel();
    }

    private void Update()
    {
        switch (WheelState)
        {
            case WheelStates.Rotation:
                RotateWheel();
                break;
            case WheelStates.Stop:
                StopWheel();
                break;
            case WheelStates.Idle:
                return;
        }
    }

    private void IntializeWheel()
    {
        for (int i = 0; i < prizesDB.WheelRewards.Count; i++)
        {

        }

        int angle = 360 / Prizes.Length - 1;
        int count = Prizes.Length / 2;

        for (int i = 1; i < count; i++)
        {
            GameObject sprite = Instantiate(Separator, ObjectForRotation.transform);
            sprite.transform.Rotate(0, 0, angle * i);
        }
    }

    public void StartWheel()
    {
        WheelState = WheelStates.Rotation;
        WheelSpeed = Random.Range(0.5f, 2);
    }

    private void RotateWheel()
    {
        DeltaTime += Time.deltaTime;
        ObjectForRotation.transform.Rotate(new Vector3(0, 0, WheelSpeed * DeltaTime));

        if (DeltaTime >= RotationTime)
            WheelState = WheelStates.Stop;
    }

    private void StopWheel()
    {
        DeltaTime -= Time.deltaTime;
        ObjectForRotation.transform.Rotate(new Vector3(0, 0, WheelSpeed * DeltaTime));

        if (DeltaTime <= 0)
            WheelState = WheelStates.Idle;
    }

    private void GivePrize()
    {
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
    }


    private enum WheelStates
    {
        Rotation,
        Stop,
        Idle
    }
    //TODO Show Prize
}
