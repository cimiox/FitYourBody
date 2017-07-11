using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Muscle : MonoBehaviour 
{
	public delegate void ChangingClicks(float count);
    public abstract event ChangingClicks ChangeClicks;

    public static GameObject ZoomableGO { get; set; }
    public static float Multiplier { get; set; }
    public abstract float LocalClicks { get; set; }
    public abstract bool IsZoom { get; set; }
    public static Dictionary<GameObject, Muscle> Muscles { get; set; }

    private int[] MuscleExperience = new int[10] 
    {10, 5000, 11000, 17000, 23000, 29000, 35000, 43000, 47000, 55000};

	protected abstract void OnMouseDown();
    protected abstract void Initialize();
    protected abstract void MuscleLevelUp(int muscleLevel, GameObject parent);
    protected abstract Dictionary<GameObject, Muscle> SetMusclesInList(int muscleLevel, GameObject parent);

    protected int GetMuscleExperience(int level)
    {
        return MuscleExperience[level - 1];
    }
}
