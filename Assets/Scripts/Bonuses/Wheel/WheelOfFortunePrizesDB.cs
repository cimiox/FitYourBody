using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortunePrizesDB : ScriptableObject
{
    [SerializeField]
    private List<WheelReward> wheelRewards;
    public List<WheelReward> WheelRewards
    {
        get { return wheelRewards; }
        set { wheelRewards = value; }
    }
}
