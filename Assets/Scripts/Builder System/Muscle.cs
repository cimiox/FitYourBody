using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Muscle : MonoBehaviour
{
    public delegate void ChangingClicks(float count);
    public event ChangingClicks ChangeClicks;

    public static GameObject ZoomableGO { get; set; }
    public static float Multiplier { get; set; }
    public bool IsZoom { get; set; }
    public int MuscleLevel { get; set; } = 1;

    private float localClicks;
    public float LocalClicks
    {
        get
        {
            return localClicks;
        }
        set
        {
            if (ChangeClicks != null)
                ChangeClicks(value - localClicks);

            localClicks = value;
        }
    }

    public static List<MuscleItems> Muscles { get; set; } = new List<MuscleItems>();

    private int[] MuscleExperience = new int[10]
    {5, 10, 15, 20, 25, 29000, 35000, 43000, 47000, 55000};


    protected abstract void Initialize();
    protected abstract void MuscleLevelUp(int muscleLevel, List<MuscleItems> list);
    protected abstract List<MuscleItems> SetMusclesInList(GameObject parent);

    protected virtual void OnMouseDown()
    {
        ZoomableGO = gameObject;

        if (!IsZoom)
        {
            Initialize();
            return;
        }

        LocalClicks += Convert.ToInt32(1 * Multiplier);

        if (localClicks >= GetMuscleExperience(MuscleLevel))
            MuscleLevelUp(MuscleLevel, Muscles);
    }

    protected int GetMuscleExperience(int level)
    {
        return MuscleExperience[level - 1];
    }

    protected void AddMuscles(List<MuscleItems> list)
    {
        foreach (var item in list)
        {
            //TODO:  Переделать под сохранение
            if (item.Muscle.MuscleLevel != 1)
            {
                item.MuscleGO.SetActive(false);
            }

            Muscles.Add(new MuscleItems(item.MuscleGO, item.Muscle));
        }
    }

    public struct MuscleItems
    {
        public GameObject MuscleGO { get; set; }
        public Muscle Muscle { get; set; }

        public MuscleItems(GameObject muscleGO, Muscle muscle)
        {
            MuscleGO = muscleGO;
            Muscle = muscle;
        }
    }
}
