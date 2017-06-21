using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public static int Money { get; set; }
    private static float level;
    public static float Level
    {
        get { return level;}
        set 
        { 
            level = value;
            ClickManager.experience.value = 0f;
        }
    }
    
	public static float NowExperience { get; set; }	
	private readonly float StartExp = 150;
	
	public float GetExp(float level)
    {
        if (level <= 1)
            return StartExp;

        return StartExp * level + GetExp(level - 1);
    }

	public void UpLevel(Slider slider, float sumClicks)
    {
        slider.maxValue = GetExp(Level);
        
		if(sumClicks < slider.maxValue)
		{
            slider.value += sumClicks;
            //StartCoroutine(UpLevelAnimation(slider, sumClicks, 1f));
		}
        else
		{
            slider.value += slider.maxValue;
            //StartCoroutine(UpLevelAnimation(slider, slider.maxValue, 1f));
            Level++;
            UpLevel(slider, sumClicks - slider.maxValue);
		}
    }

	public void MuscleSystem_ChangeClicks(Slider slider)
    {
        //clicksText.text = string.Format("Clicks: {0}\nLevel: {1}", MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks, PlayerAttributes.Expirience.Level);
		UpLevel(slider, MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks);
	}

    private IEnumerator UpLevelAnimation(Slider slider, float value, float speed)
    {
        do
        {
            value -= speed > value ? speed = value : speed;
            slider.value += speed;
            yield return new WaitForSecondsRealtime(0.1f);
            speed *= 1.5f;
        } while (value > 0);
    }

	private float GetClicks(GameObject parentGO)
    {
        float sum = 0;
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
