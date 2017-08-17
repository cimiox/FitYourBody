using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMuscle : Muscle
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        TypeMuscle = MuscleTypes.Chest;

        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList<ChestMuscle>(transform.parent.transform.parent.gameObject));

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
            if (item.Muscle is ChestMuscle)
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
