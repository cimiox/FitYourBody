using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SportingGoodsBoost : Boost
{
    public Muscle.MuscleTypes MuscleTypeForBoost { get; set; }

    public SportingGoodsBoost(Timer timer, Item properties) : base(timer, properties)
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

            BoostTimer.NowTime = DateTime.Now;

            yield return new WaitForSeconds(1f);
        }
    }
}
