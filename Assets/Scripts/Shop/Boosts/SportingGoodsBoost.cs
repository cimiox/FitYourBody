using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SportingGoodsBoost : Boost
{
    public Muscle.MuscleTypes MuscleTypeForBoost { get; set; }

    public SportingGoodsBoost(Timer timer) : base(timer)
    {
    }

    public override IEnumerator TimerEnumerator()
    {
        var muscleForBoost = PlayerAttributes.PlayerProperties.Muscles.First(x => x.Value.TypeMuscle.Equals(MuscleTypeForBoost)).Value;
        var multiplier = (Properties as SportingGoodsItem).Multiplier;

        while (true)
        {
            if (!PlayerAttributes.PlayerProperties.Muscles.Values.Contains(muscleForBoost))
                muscleForBoost = PlayerAttributes.PlayerProperties.Muscles.First(x => x.Value.TypeMuscle.Equals(MuscleTypeForBoost)).Value;

            muscleForBoost.LocalClicks += multiplier;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
