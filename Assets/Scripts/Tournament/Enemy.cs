using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Dictionary<Muscle.MuscleTypes, int> MuscleLevels = new Dictionary<Muscle.MuscleTypes, int>();
    public List<Muscle> Muscles { get; set; } = new List<Muscle>();
    public int Scores { get; set; }
    private GameObject enemyGO;
    public GameObject EnemyGO
    {
        get { return enemyGO; }
        set
        {
            enemyGO = value;

            SetMuscles();
        }
    }

    private void SetMuscles()
    {
        var muscles = EnemyGO.GetComponentsInChildren<Muscle>();

        for (int i = 0; i < muscles.Length; i++)
        {
            switch (muscles[i].TypeMuscle)
            {
                case Muscle.MuscleTypes.Ass:
                    muscles[i].MuscleLevel = GetMuscleLevel<AssMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.Chest:
                    muscles[i].MuscleLevel = GetMuscleLevel<ChestMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.LegsFront:
                    muscles[i].MuscleLevel = GetMuscleLevel<LegsMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.LegsBack:
                    if (EnemyGO.GetComponentsInChildren<BackLegsMuscle>().Max(x => x.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<BackLegsMuscle>();
                    break;
                case Muscle.MuscleTypes.HandsFront:
                    if (EnemyGO.GetComponentsInChildren<HandsMuscles>().Max(x => x.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<HandsMuscles>();
                    break;
                case Muscle.MuscleTypes.HandsBack:
                    if (EnemyGO.GetComponentsInChildren<BackHandsMuscle>().Max(x => x.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<BackHandsMuscle>();
                    break;
                case Muscle.MuscleTypes.Press:
                    muscles[i].MuscleLevel = GetMuscleLevel<PressMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.Back:
                    muscles[i].MuscleLevel = GetMuscleLevel<BackMuscle>(EnemyGO) + 1;
                    break;
            }

            Muscles.Add(muscles[i]);
        }

        for (int i = 0; i < Muscles.Count; i++)
        {
            ActivateMuscle(Muscles[i]);
        }
    }

    private void ActivateMuscle(Muscle muscle)
    {
        if (muscle.MuscleLevel != MuscleLevels.Where(x => x.Key == muscle.TypeMuscle).First().Value)
            muscle.gameObject.SetActive(false);
    }

    private void SetLevelDoubledMuscles<T>() where T : Muscle
    {
        var doubledMuscles = EnemyGO.GetComponentsInChildren<T>();
        int muscleLevel = 0;

        for (int i = 0; i < doubledMuscles.Length; i++)
        {
            if ((i + 1) % 2 == 0)
                doubledMuscles[i].MuscleLevel = doubledMuscles[i - 1].MuscleLevel;
            else
                doubledMuscles[i].MuscleLevel = ++muscleLevel;
        }
    }

    private int GetMuscleLevel<T>(GameObject parent) where T : Muscle
    {
        return parent.GetComponentsInChildren<T>().Max(x => x.MuscleLevel);
    }
}
