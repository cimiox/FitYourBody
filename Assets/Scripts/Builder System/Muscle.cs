using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Muscle : MonoBehaviour
{
    public delegate void ChangingClicks(float count);

    public delegate void MuscleChanged(Muscle muscle);
    public static event MuscleChanged OnMuscleChanged;

    private bool isEnemy;
    public bool IsEnemy
    {
        get { return isEnemy =
                transform.parent.parent.GetComponentInParent<Enemy>() == null ? isEnemy = false : isEnemy = true; }
    }

    public bool IsZoom { get; set; }
    public bool IsDouble { get; set; }
    public Attributes Properties { get; set; }

    protected virtual void Initialize()
    {
        ZoomSystem.Zoom(PlayerAttributes.ZoomableGO);
        IsZoom = true;
    }

    protected virtual void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!IsEnemy)
            {
                if (!TournamentHandler.IsTournamentStart)
                {
                    PlayerAttributes.ZoomableGO = gameObject;
                    if (!IsZoom)
                    {
                        Initialize();
                        return;
                    }

                    Properties.LocalClicks += Convert.ToInt32(1 * PlayerAttributes.PlayerProperties.Multiplier);

                    if (Properties.LocalClicks >= GetMuscleExperience(Properties.MuscleLevel))
                    {
                        OnMuscleChanged?.Invoke(MuscleController.MuscleLevelUp(this));
                    }
                }
            }
        }
    }

    protected int GetMuscleExperience(int level)
    {
        return PlayerAttributes.MuscleExperience[level - 1];
    }

    protected int GetParentLevel(Muscle muscle)
    {
        return Convert.ToInt32(muscle.transform.parent.name.Split('_')[1]);
    }

    protected float GetLocalClicks(Attributes attributes)
    {
        var value = PlayerAttributes.PlayerProperties
            .Muscles
            .First(x => x.Key == attributes.TypeMuscle)
            .Value;
        return attributes.MuscleLevel == value.MuscleLevel ? value.LocalClicks : 0;
    }

    protected Attributes SetAttributes(MuscleTypes type)
    {
        if (!IsEnemy)
        {
            var attributes = new Attributes(type, GetParentLevel(this));
            if (GetParentLevel(this) != PlayerAttributes.PlayerProperties.Muscles.First(x => x.Key == type).Value.MuscleLevel)
            {
                attributes.LocalClicks = GetLocalClicks(attributes);
                return attributes;
            }
            else
                return PlayerAttributes.PlayerProperties.Muscles.First(x => x.Key == type).Value;
        }
        //TODO write for enemy
        return null;
    }

    public class Attributes
    {
        public event ChangingClicks OnClicksChanging;

        private float localClicks;
        public float LocalClicks
        {
            get
            {
                return localClicks;
            }
            set
            {
                OnClicksChanging?.Invoke(value - localClicks);

                localClicks = value;

                if (PlayerAttributes.IsCanSave)
                    SerializationSystem.Save(PlayerAttributes.PlayerProperties);
            }
        }

        public int MuscleLevel { get; set; } = 1;

        public MuscleTypes TypeMuscle { get; set; }

        public Attributes()
        {

        }

        public Attributes(int muscleLevel)
        {
            MuscleLevel = muscleLevel;
        }

        public Attributes(MuscleTypes type, int muscleLevel)
        {
            TypeMuscle = type;
            MuscleLevel = muscleLevel;
        }

        public Attributes(Attributes clone)
        {
            LocalClicks = clone.LocalClicks;
            TypeMuscle = clone.TypeMuscle;
            MuscleLevel = clone.MuscleLevel;
        }
    }

    public enum MuscleTypes
    {
        Ass,
        Chest,
        LegsFront,
        LegsBack,
        HandsFront,
        HandsBack,
        Press,
        Back,
    }
}
