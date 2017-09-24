using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    public override void Awake()
    {
        TypeMuscle = MuscleTypes.Ass;

        if (!IsEnemy)
        {
            if (!IsCalled)
            {
                AddMuscles(SetMusclesInList<AssMuscle>(gameObject.transform.parent.parent.gameObject));

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
            if (item.Muscle is AssMuscle)
            {
                if (item.Muscle.MuscleLevel == (muscleLevel + 1))
                {
                    item.MuscleGO.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
        ZoomSystem.Detach();
    }
}
