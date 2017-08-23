using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public delegate void LevelChanged();
    public static event LevelChanged OnLevelChanged;

    public delegate void MoneyChanged();
    public static event MoneyChanged OnMoneyChanged;

    private static double money = -1;
    public static double Money
    {
        get
        {
            return money = money == -1 ? PlayerPrefs.GetFloat("PlayerMoney", 0) : money;
        }
        set
        {
            money = value;
            PlayerPrefs.SetFloat("PlayerMoney", Convert.ToSingle(value));
            OnMoneyChanged?.Invoke();
        }
    }

    private static int level;
    public static int Level
    {
        get
        {
            return level = level == 0 ? PlayerPrefs.GetInt("PlayerLevel", 1) : level;
        }
        set
        {
            level = value;
            PlayerPrefs.SetInt("PlayerLevel", value);
            OnLevelChanged();
        }
    }

    public static float NowExperience { get; set; }
    private readonly float StartExp = 150;

    private void Awake()
    {
        OnLevelChanged += LevelChanged_OnLevelChanged;
        Muscle.OnMuscleChanged += Muscle_OnMuscleChanged;
    }

    private void Muscle_OnMuscleChanged(Muscle.MuscleTypes type)
    {
        foreach (var item in Muscle.Muscles)
        {
            if (item.Muscle.TypeMuscle == type && item.MuscleGO.activeInHierarchy)
            {
                PlayerPrefs.SetInt(string.Format("{0}{1}", type.ToString(), "Muscle"), item.Muscle.MuscleLevel);
            }
        }
    }

    private void LevelChanged_OnLevelChanged()
    {
        ClickManager.experience.value = 0f;
        ClickManager.experience.maxValue = GetExpForNextLVL(level);
    }

    public float GetExpForNextLVL(float level)
    {
        if (level <= 1)
            return StartExp;

        return StartExp * level + GetExpForNextLVL(level - 1);
    }

    public void UpLevel(Slider slider, float countClicks)
    {
        if (slider.value >= slider.maxValue)
            Level++;
        else
        {
            if (countClicks < slider.maxValue)
            {
                slider.value += countClicks;
                //StartCoroutine(UpLevelAnimation(slider, countClicks, 1f));
            }
            else if (countClicks > slider.maxValue)
            {
                slider.value += slider.maxValue;
                //StartCoroutine(UpLevelAnimation(slider, slider.maxValue, 1f));
                Level++;
                UpLevel(slider, countClicks - slider.maxValue);
            }
        }
    }

    public void Muscle_ChangeClicks(float count)
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

        if (childrensWithComponent == null)
            return 0;

        foreach (var item in childrensWithComponent)
            sum += item.Clicks;

        return sum;
    }

    public static bool RemoveMoney(double count)
    {
        if (count <= Money)
        {
            Money -= count;
            return true;
        }
        else
            return false;
    }
}
