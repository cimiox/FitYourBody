using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsMuscles : Muscle 
{
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
    public override int MuscleLevel { get; set; }

    public override event ChangingClicks ChangeClicks;
    public static bool IsCalled { get; set; }

    private void Awake()
    {
        if (!IsCalled)
        {
            AddMuscles(SetMusclesInList(gameObject.transform.parent.transform.parent.gameObject));

            IsCalled = true;
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
            MuscleLevelUp(MuscleLevel, Muscles);
    }

    protected override void MuscleLevelUp(int muscleLevel, List<MuscleItems> list)
    {
        foreach(var item in list)
        {
            if (item.Muscle is LegsMuscles)
            {
                if (item.Muscle.MuscleLevel == (muscleLevel + 1))
                {
                    item.MuscleGO.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    protected override List<MuscleItems> SetMusclesInList(GameObject parent)
    {
        var muscles = new List<MuscleItems>();

        int muscleLevel = 0;

        foreach (var item in parent.GetComponentsInChildren<LegsMuscles>())
        {
            item.MuscleLevel = ++muscleLevel;
            muscles.Add(new MuscleItems(item.gameObject, item));
        }
        return muscles;
    }
}
