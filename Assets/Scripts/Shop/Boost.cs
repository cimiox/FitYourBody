using System;
using UnityEngine;

[Serializable]
public class Boost
{
    public delegate void TickHandler(Boost boost);
    public event TickHandler OnTickHandler;

    private bool isFirstCall = true;

    public Item Properties { get; set; }
    [SerializeField]
    private TimeSpan nowTime;
    public TimeSpan NowTime
    {
        get { return nowTime; }
        set
        {
            if (isFirstCall)
            {
                isFirstCall = false;
                OnTickHandler?.Invoke(this);
            }
            nowTime = value;
            BoughtHandler.Save();
        }
    }
    public DateTime EndTime { get; set; }
}
