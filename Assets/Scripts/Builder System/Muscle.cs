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

    private static float multiplier;
    public static float Multiplier
    {
        get
        {
            return multiplier = multiplier == 0f ? PlayerPrefs.GetFloat("Multiplier", 1f) : multiplier;
        }
        set
        {
            multiplier = value;
            PlayerPrefs.SetFloat("Multiplier", value);
        }
    }

    private bool isEnemy;
    public bool IsEnemy
    {
        get { return isEnemy = transform.parent.parent.GetComponentInParent<Enemy>() == null ? isEnemy = false : isEnemy = true; }
    }

    public bool IsZoom { get; set; }
    public int MuscleLevel { get; set; }

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

    public bool IsDouble { get; set; }
    public MuscleTypes TypeMuscle { get; set; }

    private int[] MuscleExperience = new int[10]
    {5, 10, 15, 20, 5000000, 29000, 35000, 43000, 47000, 55000};

    public virtual void Awake()
    {
    }
    protected abstract void Initialize();
    protected abstract void MuscleLevelUp(int muscleLevel, List<MuscleItem> list);

    protected virtual void OnMouseDown()
    {
        if (!IsEnemy)
        {
            if (!TournamentHandler.IsTournamentStart)
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
                    MuscleLevelUp(MuscleLevel, PlayerAttributes.Muscles);
                }
            }
        }
    }

    protected int GetMuscleExperience(int level)
    {
        return MuscleExperience[level - 1];
    }

    protected void AddMuscles(List<MuscleItem> list)
    {
        foreach (var item in list)
        {
            if (item.Muscle.MuscleLevel != PlayerPrefs.GetInt(string.Format("{0}{1}", TypeMuscle.ToString(), "Muscle"), 1))
            {
                item.MuscleGO.SetActive(false);
            }

            PlayerAttributes.Muscles.Add(new MuscleItem(item.MuscleGO, item.Muscle));
        }
    }

    protected virtual List<MuscleItem> SetMusclesInList<T>(GameObject parent) where T : Muscle
    {
        var muscles = new List<MuscleItem>();

        int muscleLevel = 0;

        foreach (var item in parent.GetComponentsInChildren<T>())
        {
            item.MuscleLevel = ++muscleLevel;
            muscles.Add(new MuscleItem(item.gameObject, item));
        }

        return muscles;
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
