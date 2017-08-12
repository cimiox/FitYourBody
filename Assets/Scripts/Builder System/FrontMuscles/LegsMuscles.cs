using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsMuscles : Muscle 
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList<LegsMuscles>(gameObject.transform.parent.transform.parent.gameObject));

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
        foreach(var item in list)
        {
            if (item.Muscle is LegsMuscles)
            {
                item.MuscleGO.SetActive(item.Muscle.MuscleLevel == ++muscleLevel ? true : false);
            }
        }
    }
}
