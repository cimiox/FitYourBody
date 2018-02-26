using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public abstract class Boost
{
    public Item Properties { get; set; }
    public Timer BoostTimer { get; set; }

    //[SerializeField]
    //private double nowTime;
    //public double NowTime
    //{
    //    get { return Math.Truncate(nowTime); }
    //    set
    //    {
    //        if (isFirstCall)
    //        {
    //            isFirstCall = false;
    //            OnTickHandler?.Invoke(this);
    //        }
    //        nowTime = value;
    //        BoostDatabase.Save();
    //    }
    //}
    //public DateTime EndTime { get; set; }

    public abstract IEnumerator TimerEnumerator();

    public Boost()
    {
        if (Properties != null)
            PlayerAttributes.PlayerProperties.Multiplier += (Properties as SportNutritionItem).Multiplier;
    }

    ~Boost()
    {
        if (Properties != null)
            PlayerAttributes.PlayerProperties.Multiplier -= (Properties as SportNutritionItem).Multiplier;
    }

    public class Timer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TimeSpan nowTime;
        public TimeSpan NowTime
        {
            get { return nowTime; }
            set
            {
                nowTime = value;
                OnPropertyChanged("NowTime");
            }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
