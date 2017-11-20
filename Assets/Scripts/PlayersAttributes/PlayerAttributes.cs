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

    private static Slider experienceSlider;
    public static Slider ExperienceSlider
    {
        get
        {
            return experienceSlider = experienceSlider == null
                ? GameObject.Find("Experience").GetComponent<Slider>()
                : experienceSlider;
        }
    }

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

        private int level = 1;
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


        private float experience;
        public float Experience
        {
            get { return experience; }
            set
            {
                experience = value;

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
        SetBackgroundBlur();

        SerializationSystem.Save(PlayerProperties);
        IsCanSave = true;

        ExperienceSlider.maxValue = GetExpForNextLVL(PlayerProperties.Level);
        ExperienceSlider.value = PlayerProperties.Experience;

        Properties.OnLevelChanged += LevelChanged_OnLevelChanged;
        Muscle.OnMuscleChanged += Muscle_OnMuscleChanged;
        Muscle.Attributes.OnClicksChanging += Muscle_ChangeClicks;
    }

    private void Start()
    {
        StartCoroutine(WaitMuscleIntialized());
    }

    private void SetBackgroundBlur()
    {
        ZoomSystem.BackgroundBlur = GameObject.Find("BackgroundBlur");
        ZoomSystem.BackgroundBlur.SetActive(false);
    }

    private IEnumerator WaitMuscleIntialized()
    {
        yield return new WaitUntil(() => FindObjectsOfType<Muscle>().ToList().TrueForAll(x => x.Properties != null));
        MuscleController.Intialize();
    }

    private void Muscle_OnMuscleChanged(Muscle muscle)
    {
        PlayerProperties.Muscles[muscle.Properties.TypeMuscle] = muscle.Properties;
    }

    private void LevelChanged_OnLevelChanged()
    {
        ExperienceSlider.value = 0f;
        ExperienceSlider.maxValue = GetExpForNextLVL(PlayerProperties.Level);
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
                slider.value += countClicks;
            else if (countClicks > slider.maxValue)
            {
                slider.value += slider.maxValue;
                PlayerProperties.Level++;
                UpLevel(slider, countClicks - slider.maxValue);
            }
        }
    }

    public void Muscle_ChangeClicks(float count)
    {
        UpLevel(ExperienceSlider, count);
        PlayerProperties.Experience += count;
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

    public void Detach()
    {
        ZoomSystem.Detach();
    }

    public static void ActivateBlur(Muscle muscle)
    {
        foreach (var item in MuscleController.Muscles)
        {
            if (item.gameObject.activeInHierarchy && item != muscle)
            {
                var blur = Instantiate(Resources.Load<GameObject>("PlayerBlur"), Vector3.zero, Quaternion.identity, item.transform);
                blur.transform.localPosition = new Vector3(0, 0, -1);
                blur.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            }
        }
        //TODO: REWRITE this
        try
        {
            var blurHead = Instantiate(Resources.Load<GameObject>("PlayerBlur"), Vector3.zero, Quaternion.identity, GameObject.Find("Head_S").transform);
            blurHead.transform.localPosition = new Vector3(0, 0, -1);
            blurHead.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Head_S").GetComponent<SpriteRenderer>().sprite;
        }
        catch
        {
            var blurHead = Instantiate(Resources.Load<GameObject>("PlayerBlur"), Vector3.zero, Quaternion.identity, GameObject.Find("Head Back").transform);
            blurHead.transform.localPosition = new Vector3(0, 0, -1);
            blurHead.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Head Back").GetComponent<SpriteRenderer>().sprite;
        }
    }

    public static void DeactivateBlur()
    {
        foreach (var item in MuscleController.Muscles)
        {
            try
            {
                if (item.transform.childCount > 0)
                    Destroy(item.transform.Find("PlayerBlur(Clone)").gameObject);
            }
            catch
            {
                continue;
            }
        }

        //TODO: REWRITE this
        try
        {
            Destroy(GameObject.Find("Head_S").transform.GetChild(0).gameObject);
        }
        catch
        {
            Destroy(GameObject.Find("Head Back").transform.GetChild(0).gameObject);
        }
        
    }
}
