using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerAttributes 
{
    public static Experience Experience { get; set; }
    public static int Money { get; set; }

    public static void Init()
    {
        Experience = new Experience();
    }
}
