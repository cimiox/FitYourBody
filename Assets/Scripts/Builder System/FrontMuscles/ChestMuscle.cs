using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMuscle : Muscle
{
    public override event ChangingClicks ChangeClicks;

	private float localClicks;
    public override float LocalClicks 
	{ 
		get
		{
			return localClicks;
		} 
		set
		{
            if (ChangeClicks != null)
                ChangeClicks(value - localClicks);
                
			localClicks = value;
		}
	}
	public override bool IsZoom { get; set; }
    public int MuscleLevel;

    private void Awake()
    {
        //TODO: Block Many calles
        foreach (var item in SetMusclesInList(MuscleLevel, gameObject.transform.parent.transform.parent.gameObject))
        {
            Muscles.Add(item.Key, item.Value);
        }
    }

    protected override void Initialize()
    {
        ZoomSystem.Zoom(ZoomableGO);
        IsZoom = true;
        Multiplier = 1;
    }

    protected override void OnMouseDown()
    {
        ZoomableGO = gameObject;

        if (!IsZoom)
        {
            Initialize();
            return;
        }

        LocalClicks += Convert.ToInt32(1 * Multiplier);
        if (localClicks >= GetMuscleExperience(MuscleLevel))
            MuscleLevelUp(MuscleLevel, gameObject.transform.parent.transform.parent.gameObject);
    }

    protected override void MuscleLevelUp(int muscleLevel, GameObject parent)
    {
        foreach(var item in parent.GetComponentsInChildren<ChestMuscle>())
        {
            if (item.MuscleLevel == ++muscleLevel)
            {
                item.gameObject.SetActive(true);
                break;
            }
        }

        gameObject.SetActive(false);
    }

    protected override Dictionary<GameObject, Muscle> SetMusclesInList(int muscleLevel, GameObject parent)
    {
        var muscles = new Dictionary<GameObject, Muscle>();

        foreach (var item in parent.GetComponentsInChildren<PressMuscle>())
        {
            muscles.Add(item.gameObject, item);
        }

        return muscles;
    }
}
