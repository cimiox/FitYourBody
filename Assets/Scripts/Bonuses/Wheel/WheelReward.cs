using System;
using UnityEngine;

[Serializable]
public class WheelReward
{
    [SerializeField]
    private WheelRewardType rewardType;
    public WheelRewardType RewardType
    {
        get { return rewardType; }
        set { rewardType = value; }
    }

    [SerializeField]
    private int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    [SerializeField]
    private string reward;
    public string Reward
    {
        get { return reward; }
        set { reward = value; }
    }
}
