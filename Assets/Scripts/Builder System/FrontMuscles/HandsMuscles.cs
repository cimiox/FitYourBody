using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandsMuscles : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.HandsFront);
    }
}
