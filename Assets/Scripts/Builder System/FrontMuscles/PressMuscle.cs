using System.Collections.Generic;

public class PressMuscle : Muscle 
{
    public void Start()
    {
        Properties = SetAttributes(MuscleTypes.Press);
    }
}
