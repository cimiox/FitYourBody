﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDisposable
{
    public Dictionary<Muscle.MuscleTypes, int> MuscleLevels = new Dictionary<Muscle.MuscleTypes, int>();
    
    public int Scores { get; set; }
    private readonly string EnemyPath = "Tournament/Enemy";

    public GameObject EnemyGO { get; set; }
    public List<Muscle> Muscles { get; set; } = new List<Muscle>();

    public void Awake()
    {
        GameObject tournament = GameObject.Find("Tournament");

        EnemyGO = Instantiate(Resources.Load<GameObject>(EnemyPath), tournament.transform);
        EnemyGO.name = string.Format("Enemt[{0}]", tournament.transform.childCount);

        MuscleLevels = TournamentHandler.GetEnemyMusclesLevels(this);

        SetMuscles();
    }

    private void SetMuscles()
    {
        var muscles = EnemyGO.GetComponentsInChildren<Muscle>();

        for (int i = 0; i < muscles.Length; i++)
        {
            switch (muscles[i].Properties.TypeMuscle)
            {
                case Muscle.MuscleTypes.Ass:
                    muscles[i].Properties.MuscleLevel = GetMuscleLevel<AssMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.Chest:
                    muscles[i].Properties.MuscleLevel = GetMuscleLevel<ChestMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.LegsFront:
                    muscles[i].Properties.MuscleLevel = GetMuscleLevel<LegsMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.LegsBack:
                    if (EnemyGO.GetComponentsInChildren<BackLegsMuscle>().Max(x => x.Properties.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<BackLegsMuscle>();
                    break;
                case Muscle.MuscleTypes.HandsFront:
                    if (EnemyGO.GetComponentsInChildren<HandsMuscles>().Max(x => x.Properties.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<HandsMuscles>();
                    break;
                case Muscle.MuscleTypes.HandsBack:
                    if (EnemyGO.GetComponentsInChildren<BackHandsMuscle>().Max(x => x.Properties.MuscleLevel) == 0)
                        SetLevelDoubledMuscles<BackHandsMuscle>();
                    break;
                case Muscle.MuscleTypes.Press:
                    muscles[i].Properties.MuscleLevel = GetMuscleLevel<PressMuscle>(EnemyGO) + 1;
                    break;
                case Muscle.MuscleTypes.Back:
                    muscles[i].Properties.MuscleLevel = GetMuscleLevel<BackMuscle>(EnemyGO) + 1;
                    break;
            }
        }

        for (int i = 0; i < muscles.Length; i++)
        {
            ActivateMuscle(muscles[i]);
            if (muscles[i].gameObject.activeSelf)
                Muscles.Add(muscles[i]);
        }

        EnemyGO.transform.Find("Enemy_Back").gameObject.SetActive(false);
    }

    private void ActivateMuscle(Muscle muscle)
    {
        if (muscle.Properties.MuscleLevel != MuscleLevels.Where(x => x.Key == muscle.Properties.TypeMuscle).First().Value)
            muscle.gameObject.SetActive(false);
    }

    private void SetLevelDoubledMuscles<T>() where T : Muscle
    {
        var doubledMuscles = EnemyGO.GetComponentsInChildren<T>();
        int muscleLevel = 0;

        for (int i = 0; i < doubledMuscles.Length; i++)
        {
            if ((i + 1) % 2 == 0)
                doubledMuscles[i].Properties.MuscleLevel = doubledMuscles[i - 1].Properties.MuscleLevel;
            else
                doubledMuscles[i].Properties.MuscleLevel = ++muscleLevel;
        }
    }

    private int GetMuscleLevel<T>(GameObject parent) where T : Muscle
    {
        return parent.GetComponentsInChildren<T>().Max(x => x.Properties.MuscleLevel);
    }

    public void Dispose()
    {
        EnemyGO.transform.Find("Enemy_Back").gameObject.SetActive(true);

        EnemyGO.GetComponentsInChildren<Muscle>().ToList().ForEach(x => x.gameObject.SetActive(true));
        GC.SuppressFinalize(this);
    }
}
