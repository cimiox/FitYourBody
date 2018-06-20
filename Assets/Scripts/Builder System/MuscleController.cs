using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MuscleController
{
    public static List<Muscle> Muscles { get; set; } = new List<Muscle>();

    public static void Intialize()
    {
        Muscles.AddRange(GameObject.FindObjectsOfType<Muscle>());

        foreach (var item in Muscles)
        {
            item.gameObject.SetActive(item.Properties.MuscleLevel == 
                PlayerAttributes.PlayerProperties.Muscles
                .First(x => x.Key == item.Properties.TypeMuscle).Value.MuscleLevel);
        }
    }

    public static Muscle MuscleLevelUp(Muscle muscle)
    {
        ZoomSystem.Detach();

        Muscles
            .Where(x => x.Properties.TypeMuscle == muscle.Properties.TypeMuscle)
            .Where(x => x.Properties.MuscleLevel == muscle.Properties.MuscleLevel)
            .ToList()
            .ForEach(x => x.gameObject.SetActive(false));

        Muscles
            .Where(x => x.Properties.TypeMuscle == muscle.Properties.TypeMuscle)
            .Where(x => x.Properties.MuscleLevel == muscle.Properties.MuscleLevel + 1)
            .ToList()
            .ForEach(x => x.gameObject.SetActive(true));

        SerializationSystem.Save(PlayerAttributes.PlayerProperties);

        return Muscles
            .Where(x => x.Properties.TypeMuscle == muscle.Properties.TypeMuscle)
            .First(x => x.gameObject.activeSelf);
    }
}
