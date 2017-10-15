using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackLegsMuscle : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.LegsBack);
    }
}
