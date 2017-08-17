using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Muscle : MonoBehaviour
{
    public delegate void ChangingClicks(float count);
    public event ChangingClicks OnClicksChanging;

    public delegate void MuscleChanged(MuscleTypes type);
    public static event MuscleChanged OnMuscleChanged;

    public static GameObject ZoomableGO { get; set; }

    private static int multiplier;
    public static int Multiplier
    {
        get {
            return multiplier = multiplier == 0 ? PlayerPrefs.GetInt("Multiplier", 1) : multiplier; }
        set
        {
            multiplier = value;
            PlayerPrefs.SetInt("Multiplier", value);
        }
    }

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
            OnClicksChanging?.Invoke(value - localClicks);

            localClicks = value;
        }
    }

    public MuscleTypes TypeMuscle { get; set; }

    public static List<MuscleItems> Muscles { get; set; } = new List<MuscleItems>();

    private int[] MuscleExperience = new int[10]
    {5, 10, 15, 20, 5000000, 29000, 35000, 43000, 47000, 55000};


    protected abstract void Initialize();
    protected abstract void MuscleLevelUp(int muscleLevel, List<MuscleItems> list);

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
        {
            OnMuscleChanged?.Invoke(TypeMuscle);
            MuscleLevelUp(MuscleLevel, Muscles);
        }
    }

    protected int GetMuscleExperience(int level)
    {
        return MuscleExperience[level - 1];
    }

    protected void AddMuscles(List<MuscleItems> list)
    {
        foreach (var item in list)
        {
            if (item.Muscle.MuscleLevel != PlayerPrefs.GetInt(string.Format("{0}{1}", TypeMuscle.ToString(), "Muscle"), 1))
            {
                item.MuscleGO.SetActive(false);
            }

            Muscles.Add(new MuscleItems(item.MuscleGO, item.Muscle));
        }
    }

    protected virtual List<MuscleItems> SetMusclesInList<T>(GameObject parent) where T : Muscle
    {
        var muscles = new List<MuscleItems>();

        int muscleLevel = 0;

        foreach (var item in parent.GetComponentsInChildren<T>())
        {
            item.MuscleLevel = ++muscleLevel;
            muscles.Add(new MuscleItems(item.gameObject, item));
        }

        return muscles;
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

    public enum MuscleTypes
    {
        Ass,
        Chest,
        LegsFront,
        LegsBack,
        HandsFront,
        HandsBack,
        Press,
        Back,
        
    }
}
