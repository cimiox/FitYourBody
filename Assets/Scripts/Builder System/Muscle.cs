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
    public abstract bool isZoom { get; set; }

	protected abstract void OnMouseDown();
    protected abstract void Initialize();
    
}
