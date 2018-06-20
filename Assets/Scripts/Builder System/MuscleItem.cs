using System;
using UnityEngine;

public struct MuscleItem
{
    public GameObject MuscleGO { get; set; }
    public Muscle Muscle { get; set; }

    public MuscleItem(GameObject muscleGO, Muscle muscle)
    {
        MuscleGO = muscleGO;
        Muscle = muscle;
    }
}
