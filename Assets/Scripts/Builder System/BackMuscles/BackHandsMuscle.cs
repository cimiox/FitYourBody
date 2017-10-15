using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BackHandsMuscle : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.HandsBack);
    }
}
