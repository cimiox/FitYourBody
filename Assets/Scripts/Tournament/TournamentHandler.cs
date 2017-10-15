using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TournamentHandler : MonoBehaviour
{
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();

    public int PlayerScores { get; set; }
    public static bool IsTournamentStart { get; set; }

    private readonly int MaxEnemies = 2;
    private readonly static int MaxMuscleLevel = 5;

    private void OnEnable()
    {
        IsTournamentStart = true;
        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void OnDisable()
    {
        IsTournamentStart = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Initialize()
    {
        PlayerScores = 0;
        Enemies.ForEach(x => x.Dispose());
        Enemies.Clear();

        for (int i = 0; i < MaxEnemies; i++)
        {
            Enemies.Add(new Enemy());
        }

        GetScores();

        print(PlayerScores);
        for (int i = 0; i < Enemies.Count; i++)
        {
            print(Enemies[i].Scores);
        }
    }

    private void GetScores()
    {
        foreach (var item in Enum.GetNames(typeof(Muscle.MuscleTypes)))
        {
            List<Muscle> musclesForComparison = new List<Muscle>();

            for (int i = 0; i < Enemies.Count; i++)
            {
                musclesForComparison.Add(Enemies[i].Muscles.Where(x => x.Properties.TypeMuscle.ToString() == item).First());
            }

            SetScores(musclesForComparison, PlayerAttributes.Muscles
               .Where(x => x.MuscleGO.activeSelf)
               .Where(x => x.Muscle.Properties.TypeMuscle.ToString() == item)
               .First().Muscle);

            musclesForComparison.Clear();
        }
    }

    private void SetScores(List<Muscle> musclesForComparison, Muscle playerMuscle)
    {
        if (playerMuscle.Properties.MuscleLevel != 1)
        {
            var maxLevel = musclesForComparison.Max(x => x.Properties.MuscleLevel);

            if (playerMuscle.Properties.MuscleLevel > maxLevel)
            {
                PlayerScores++;
            }
            else if (playerMuscle.Properties.MuscleLevel == maxLevel)
            {
                PlayerScores++;

                for (int i = 0; i < musclesForComparison.Count; i++)
                {
                    if (musclesForComparison[i].Properties.MuscleLevel == maxLevel)
                    {
                        UpEnemyScores(musclesForComparison[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < musclesForComparison.Count; i++)
                {
                    if (musclesForComparison[i].Properties.MuscleLevel == 1)
                    {
                        UpEnemyScores(musclesForComparison[i]);
                    }
                }
            }
        }
        else
        {
            PlayerScores--;

            var maxLevel = musclesForComparison.Max(x => x.Properties.MuscleLevel);

            for (int i = 0; i < musclesForComparison.Count; i++)
            {
                if (musclesForComparison[i].Properties.MuscleLevel == maxLevel)
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

    public static Dictionary<Muscle.MuscleTypes, int> GetEnemyMusclesLevels(Enemy enemy)
    {
        var playerMaxMuscleLevel = PlayerAttributes.Muscles.Max(x => x.Muscle.Properties.MuscleLevel);
        var muscleTypes = Enum.GetNames(typeof(Muscle.MuscleTypes));

        foreach (var item in muscleTypes)
        {
            enemy.MuscleLevels.Add(
                (Muscle.MuscleTypes)Enum.Parse(typeof(Muscle.MuscleTypes), item),
                GetEnemyMuscleLevel(playerMaxMuscleLevel));
        }

        return enemy.MuscleLevels;
    }

    private static int GetEnemyMuscleLevel(int playerMaxMuscleLevel)
    {
        int level = new System.Random().Next(playerMaxMuscleLevel - 3, playerMaxMuscleLevel + 3);
        return level <= 0 ? 1 : (level > MaxMuscleLevel ? 5 : level);
    }
}
