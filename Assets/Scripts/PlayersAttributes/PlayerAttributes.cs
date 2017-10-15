using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public delegate void LevelChanged();
    public delegate void MoneyChanged();

    public delegate void PlayerPropertiesLoaded();
    public static event PlayerPropertiesLoaded OnPlayerPropertiesLoaded;

    public static List<MuscleItem> Muscles { get; set; } = new List<MuscleItem>();
    public static GameObject ZoomableGO { get; set; }
    public static Properties PlayerProperties { get; set; }

    public static int[] MuscleExperience = new int[10]
    {5, 10, 15, 20, 5000000, 29000, 35000, 43000, 47000, 55000};

    private readonly float StartExp = 150;

    public static bool IsCanSave { get; set; }

    public class Properties
    {
        public static event MoneyChanged OnMoneyChanged;
        public static event LevelChanged OnLevelChanged;

        private double money;
        public double Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;

                if (IsCanSave)
                    SerializationSystem.Save(PlayerProperties);

                OnMoneyChanged?.Invoke();
            }
        }

        private int level;
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;

                if (IsCanSave)
                    SerializationSystem.Save(PlayerProperties);

                OnLevelChanged?.Invoke();
            }
        }

        private float multiplier = 1;
        public float Multiplier
        {
            get
            {
                return multiplier;
            }
            set
            {
                multiplier = value;

                if (IsCanSave)
                    SerializationSystem.Save(PlayerProperties);
            }
        }

        private Dictionary<Muscle.MuscleTypes, Muscle.Attributes> muscles = new Dictionary<Muscle.MuscleTypes, Muscle.Attributes>()
        {
            { Muscle.MuscleTypes.Ass, new Muscle.Attributes(Muscle.MuscleTypes.Ass, 1)  },
            { Muscle.MuscleTypes.Back, new Muscle.Attributes(Muscle.MuscleTypes.Back, 1) },
            { Muscle.MuscleTypes.Chest, new Muscle.Attributes(Muscle.MuscleTypes.Chest, 1) },
            { Muscle.MuscleTypes.HandsBack, new Muscle.Attributes(Muscle.MuscleTypes.HandsBack, 1) },
            { Muscle.MuscleTypes.HandsFront, new Muscle.Attributes(Muscle.MuscleTypes.HandsFront, 1) },
            { Muscle.MuscleTypes.LegsBack, new Muscle.Attributes(Muscle.MuscleTypes.LegsBack, 1) },
            { Muscle.MuscleTypes.LegsFront, new Muscle.Attributes(Muscle.MuscleTypes.LegsFront, 1) },
            { Muscle.MuscleTypes.Press, new Muscle.Attributes(Muscle.MuscleTypes.Press, 1) }
        };
        public Dictionary<Muscle.MuscleTypes, Muscle.Attributes> Muscles
        {
            get { return muscles; }
            set
            {
                muscles = value;

                if (IsCanSave)
                    SerializationSystem.Save(PlayerProperties);
            }
        }
    }

    private void Awake()
    {
        SerializationSystem.PathToProperties = Application.persistentDataPath;
        PlayerProperties = SerializationSystem.Load<Properties>();
        OnPlayerPropertiesLoaded?.Invoke();

        //SerializationSystem.Save(PlayerProperties);
        IsCanSave = true;

        Properties.OnLevelChanged += LevelChanged_OnLevelChanged;
        Muscle.OnMuscleChanged += Muscle_OnMuscleChanged;
    }

    private void Start()
    {
        StartCoroutine(WaitMuscleIntialized());
    }

    private IEnumerator WaitMuscleIntialized()
    {
        yield return new WaitUntil(() => GameObject.FindObjectsOfType<Muscle>().ToList().TrueForAll(x => x.Properties != null));
        MuscleController.Intialize();
    }

    private void Muscle_OnMuscleChanged(Muscle muscle)
    {
        PlayerProperties.Muscles[muscle.Properties.TypeMuscle] = muscle.Properties;
    }

    private void LevelChanged_OnLevelChanged()
    {
        ClickManager.experience.value = 0f;
        ClickManager.experience.maxValue = GetExpForNextLVL(PlayerProperties.Level);
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
            PlayerProperties.Level++;
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
                PlayerProperties.Level++;
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

    public static bool RemoveMoney(double count)
    {
        if (count <= PlayerProperties.Money)
        {
            PlayerProperties.Money -= count;
            return true;
        }
        else
            return false;
    }
}
