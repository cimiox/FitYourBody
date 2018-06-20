using System.Collections.Generic;
using System.Linq;

public class ChestMuscle : Muscle
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.Chest);
    }
}
