using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TournamentHandler : MonoBehaviour
{
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();

    public int PlayerScores { get; set; } = 0;

    private readonly int MaxEnemies = 2;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void OnDisable()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Initialize()
    {
        PlayerScores = 0;
        for (int i = 0; i < MaxEnemies; i++)
        {
            Enemies[i] = new Enemy();

            Enemies[i].MuscleLevels = GetEnemyMusclesLevels(Enemies[i]);

            Enemies[i].EnemyGO = transform.Find(string.Format("Enemy[{0}]", i)).gameObject;
        }
    }

    private void GetScores()
    {
        foreach (var item in Enum.GetNames(typeof(Muscle.MuscleTypes)))
        {
            List<Muscle> musclesForComparison = new List<Muscle>();

            for (int i = 0; i < Enemies.Count; i++)
            {
                musclesForComparison.Add(Enemies[i].Muscles.Where(x => x.TypeMuscle.ToString() == item).First());
            }

            SetScores(musclesForComparison, Muscle.Muscles
               .Where(x => x.MuscleGO.activeSelf)
               .Where(x => x.Muscle.TypeMuscle.ToString() == item)
               .First().Muscle);

            musclesForComparison.Clear();
        }
    }

    private void SetScores(List<Muscle> musclesForComparison, Muscle playerMuscle)
    {
        if (playerMuscle.MuscleLevel != 1)
        {
            var maxLevel = musclesForComparison.Max(x => x.MuscleLevel);

            if (playerMuscle.MuscleLevel > maxLevel)
            {
                PlayerScores++;
            }
            else if (playerMuscle.MuscleLevel == maxLevel)
            {
                PlayerScores++;

                for (int i = 0; i < musclesForComparison.Count; i++)
                {
                    if (musclesForComparison[i].MuscleLevel == maxLevel)
                    {
                        UpEnemyScores(musclesForComparison[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < musclesForComparison.Count; i++)
                {
                    if (musclesForComparison[i].MuscleLevel == 1)
                    {
                        UpEnemyScores(musclesForComparison[i]);
                    }
                }
            }
        }
        else
        {
            PlayerScores--;

            var maxLevel = musclesForComparison.Max(x => x.MuscleLevel);

            for (int i = 0; i < musclesForComparison.Count; i++)
            {
                if (musclesForComparison[i].MuscleLevel == maxLevel)
                {
                    UpEnemyScores(musclesForComparison[i]);
                }
            }
        }
    }

    private void UpEnemyScores(Muscle muscle)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            for (int j = 0; j < Enemies[i].Muscles.Count; j++)
            {
                if (Enemies[i].Muscles[j] == muscle)
                    Enemies[i].Scores++;
            }
        }
    }

    private int[] GetEnemyMusclesLevels(Enemy enemy)
    {
        var playerMaxMuscleLevel = Muscle.Muscles.Max(x => x.Muscle.MuscleLevel);

        for (int j = 0; j < enemy.MuscleLevels.Length; j++)
        {
            enemy.MuscleLevels[j] = GetEnemyMuscleLevel(playerMaxMuscleLevel);
        }

        return enemy.MuscleLevels;
    }

    private int GetEnemyMuscleLevel(int playerMaxMuscleLevel)
    {
        int level = UnityEngine.Random.Range(playerMaxMuscleLevel - 2, playerMaxMuscleLevel + 2);
        return level <= 0 ? 1 : level;
    }

    public class Enemy
    {
        public int[] MuscleLevels { get; set; } = new int[8];
        public List<Muscle> Muscles { get; set; } = new List<Muscle>();
        public int Scores { get; set; }
        private GameObject enemyGO;
        public GameObject EnemyGO
        {
            get { return enemyGO; }
            set
            {
                enemyGO = value;

                if (MuscleLevels.Min() >= 1)
                    SetMuscles();
            }
        }

        private void SetMuscles()
        {
            var muscles = EnemyGO.GetComponentsInChildren<Muscle>();

            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i].IsEnemy = true;
                switch (muscles[i].TypeMuscle)
                {
                    case Muscle.MuscleTypes.Ass:
                        if (muscles[i].MuscleLevel != MuscleLevels[0])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.Chest:
                        if (muscles[i].MuscleLevel != MuscleLevels[1])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.LegsFront:
                        if (muscles[i].MuscleLevel != MuscleLevels[2])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.LegsBack:
                        if (muscles[i].MuscleLevel != MuscleLevels[3])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.HandsFront:
                        if (muscles[i].MuscleLevel != MuscleLevels[4])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.HandsBack:
                        if (muscles[i].MuscleLevel != MuscleLevels[5])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.Press:
                        if (muscles[i].MuscleLevel != MuscleLevels[6])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    case Muscle.MuscleTypes.Back:
                        if (muscles[i].MuscleLevel != MuscleLevels[7])
                            muscles[i].gameObject.SetActive(false);
                        else
                            Muscles.Add(muscles[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
