using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public abstract class Boost
{
    public Item Properties { get; set; }
    public Timer BoostTimer { get; set; }
    public Coroutine TimerCoroutine { get; set; }

    public abstract IEnumerator TimerEnumerator();

    public Boost(Timer timer)
    {
        BoostTimer = timer;
    }

    public class Timer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime nowTime;
        public DateTime NowTime
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

        public Timer(DateTime endTime)
        {
            EndTime = endTime;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
