using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        TypeMuscle = MuscleTypes.Back;
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList<BackMuscle>(gameObject.transform.parent.transform.parent.gameObject));

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
            if (item.Muscle is BackMuscle)
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
