using System.Collections.Generic;
using System.Linq;

public class LegsMuscle : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.LegsFront);
    }
}
