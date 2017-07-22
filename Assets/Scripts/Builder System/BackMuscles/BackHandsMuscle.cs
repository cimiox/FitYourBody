using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHandsMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList(gameObject.transform.parent.transform.parent.gameObject));

            IsCalled = true;
        }
    }

    protected override void Initialize()
    {
        ZoomSystem.Zoom(ZoomableGO);
        IsZoom = true;
        Multiplier = 1;
    }

    protected override void MuscleLevelUp(int muscleLevel, List<MuscleItems> list)
    {
        foreach (var item in list)
        {
            if (item.Muscle is BackHandsMuscle)
            {
                if (item.Muscle.MuscleLevel == (muscleLevel + 1))
                {
                    item.MuscleGO.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    protected override List<MuscleItems> SetMusclesInList(GameObject parent)
    {
        var muscles = new List<MuscleItems>();

        int muscleLevel = 0;

        for (int i = 0; i < parent.GetComponentsInChildren<HandsMuscles>().Length; i++)
        {
            if ((i + 1) % 2 == 0)
                parent.GetComponentsInChildren<HandsMuscles>()[i].MuscleLevel = parent.GetComponentsInChildren<HandsMuscles>()[i - 1].MuscleLevel;
            else
                parent.GetComponentsInChildren<HandsMuscles>()[i].MuscleLevel = ++muscleLevel;

            muscles.Add(new MuscleItems(parent.GetComponentsInChildren<HandsMuscles>()[i].gameObject, parent.GetComponentsInChildren<HandsMuscles>()[i]));
        }

        return muscles;
    }
}
