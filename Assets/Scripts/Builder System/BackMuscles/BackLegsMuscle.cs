using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLegsMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        TypeMuscle = MuscleTypes.LegsBack;
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList<BackLegsMuscle>(gameObject.transform.parent.transform.parent.gameObject));

            IsCalled = true;
        }
    }

    protected override void Initialize()
    {
        ZoomSystem.Zoom(ZoomableGO);
        IsZoom = true;
    }

    protected override void MuscleLevelUp(int muscleLevel, List<MuscleItems> list)
    {
        foreach (var item in list)
        {
            if (item.Muscle is BackLegsMuscle)
            {
                item.MuscleGO.SetActive(item.Muscle.MuscleLevel == ++muscleLevel ? true : false);
            }
        }
        ZoomSystem.Detach();
    }

    protected override List<MuscleItems> SetMusclesInList<T>(GameObject parent)
    {
        var muscles = new List<MuscleItems>();

        int muscleLevel = 0;

        for (int i = 0; i < parent.GetComponentsInChildren<T>().Length; i++)
        {
            if ((i + 1) % 2 == 0)
                parent.GetComponentsInChildren<T>()[i].MuscleLevel = parent.GetComponentsInChildren<T>()[i - 1].MuscleLevel;
            else
                parent.GetComponentsInChildren<T>()[i].MuscleLevel = ++muscleLevel;

            muscles.Add(new MuscleItems(parent.GetComponentsInChildren<T>()[i].gameObject, parent.GetComponentsInChildren<T>()[i]));
        }

        return muscles;
    }
}
