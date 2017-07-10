using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMuscle : Muscle 
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
	public override bool isZoom { get; set; }

    public override event ChangingClicks ChangeClicks;

    protected override void Initialize()
    {
        ZoomSystem.Zoom(ZoomableGO);
        isZoom = true;
    }

    protected override void OnMouseDown()
    {
        ZoomableGO = gameObject;

        if (!isZoom)
        {
            Initialize();
            return;
        }

        LocalClicks += Convert.ToInt32(1 * Multiplier);
    }
}
