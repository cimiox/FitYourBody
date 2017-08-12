using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsMuscles : Muscle
{
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList<HandsMuscles>(gameObject.transform.parent.transform.parent.gameObject));

            IsCalled = true;
            //TODO: change for player prefs
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
            if (item.Muscle is HandsMuscles)
            {
                item.MuscleGO.SetActive(item.Muscle.MuscleLevel == (muscleLevel + 1) ? true : false);
            }
        }
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
