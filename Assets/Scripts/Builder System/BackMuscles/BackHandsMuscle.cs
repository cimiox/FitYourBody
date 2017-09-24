using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHandsMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    public override void Awake()
    {
        IsDouble = true;
        TypeMuscle = MuscleTypes.HandsBack;

        if (!IsEnemy)
        {
            if (!IsCalled)
            {
                AddMuscles(SetMusclesInList<BackHandsMuscle>(gameObject.transform.parent.transform.parent.gameObject));

                IsCalled = true;
            }
        }
    }

    protected override void Initialize()
    {
        ZoomSystem.Zoom(ZoomableGO);
        IsZoom = true;
    }

    protected override void MuscleLevelUp(int muscleLevel, List<MuscleItem> list)
    {
        foreach (var item in list)
        {
            if (item.Muscle is BackHandsMuscle)
            {
                item.MuscleGO.SetActive(item.Muscle.MuscleLevel == ++muscleLevel ? true : false);
            }
        }
        ZoomSystem.Detach();
    }

    protected override List<MuscleItem> SetMusclesInList<T>(GameObject parent)
    {
        var muscles = new List<MuscleItem>();

        int muscleLevel = 0;

        for (int i = 0; i < parent.GetComponentsInChildren<T>().Length; i++)
        {
            if ((i + 1) % 2 == 0)
                parent.GetComponentsInChildren<T>()[i].MuscleLevel = parent.GetComponentsInChildren<T>()[i - 1].MuscleLevel;
            else
                parent.GetComponentsInChildren<T>()[i].MuscleLevel = ++muscleLevel;

            muscles.Add(new MuscleItem(parent.GetComponentsInChildren<T>()[i].gameObject, parent.GetComponentsInChildren<T>()[i]));
        }

        return muscles;
    }
}
