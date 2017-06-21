using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
	public int Level { get; set; }
	public float NowExperience { get; set; }	
	private readonly float StartExp = 150;
	
	public float GetExp(float level)
    {
        if (level <= 1)
            return StartExp;

        return StartExp * level + GetExp(level - 1);
    }

	private void UpLevel(Slider experience, int sumClicks)
    {
		if(experience.maxValue > sumClicks)
		{
        	experience.value += sumClicks;
		}
		else
		{

		}
    }

	public void MuscleSystem_ChangeClicks(object slider)
    {
        //clicksText.text = string.Format("Clicks: {0}\nLevel: {1}", MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks, PlayerAttributes.Expirience.Level);
		StartCoroutine(UpLevelAnimation((Slider)slider, MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks, 1f));
	}

    public IEnumerator UpLevelAnimation(Slider slider, float value, float speed)
    {
        do
        {
            value -= speed > value ? speed = value : speed;
            slider.value += speed;
            yield return new WaitForSeconds(0.1f);
            speed *= 1.5f;
        } while (value > 0);
    }

	private int GetClicks(GameObject parentGO)
    {
        int sum = 0;
        var childrensWithComponent = parentGO.GetComponentsInChildren<MuscleSystem>();

        if(childrensWithComponent == null)
            return 0;

        foreach (var item in childrensWithComponent)
        {
            sum += item.Clicks;
        }

        return sum;
    }
}
