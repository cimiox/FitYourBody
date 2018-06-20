using System.Collections.Generic;
using System.Linq;

public class BackMuscle : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.Back);
    }
}
