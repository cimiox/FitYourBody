using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public delegate void LevelChanged();
    public static event LevelChanged OnLevelChanged;

    public static int Money { get; set; }
    private static int level = 1;
    public static int Level
    {
        get { return level;}
        set 
        { 
            level = value;
            OnLevelChanged();
        }
    }
    
	public static float NowExperience { get; set; }	
	private readonly float StartExp = 150;
    
    private void Awake()
    {
        OnLevelChanged += LevelChanged_OnLevelChanged;
    }

    private void LevelChanged_OnLevelChanged()
    {
        ClickManager.experience.value = 0f;
        ClickManager.experience.maxValue = GetExp(level);
    }

	public float GetExp(float level)
    {
        if (level <= 1)
            return StartExp;

        return StartExp * level + GetExp(level - 1);
    }

	public void UpLevel(Slider slider, float countClicks)
    {
        if(slider.value >= slider.maxValue)
            Level++;
        else 
        {
            if(countClicks < slider.maxValue)
            {
                slider.value += countClicks;
                //StartCoroutine(UpLevelAnimation(slider, countClicks, 1f));
            }
            else if(countClicks > slider.maxValue)
            {
                slider.value += slider.maxValue;
                //StartCoroutine(UpLevelAnimation(slider, slider.maxValue, 1f));
                Level++;
                UpLevel(slider, countClicks - slider.maxValue);
            }
        }
    }

	public void MuscleSystem_ChangeClicks(float count)
    {
        //clicksText.text = string.Format("Clicks: {0}\nLevel: {1}", MuscleSystem.ZoomableGO.GetComponent<MuscleSystem>().Clicks, PlayerAttributes.Expirience.Level);
		UpLevel(ClickManager.experience, count);
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
