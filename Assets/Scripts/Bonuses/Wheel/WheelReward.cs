using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelReward : MonoBehaviour
{
    private const string WHEEL_SPRITES_PATH = "WheelOfFortune/";

    [SerializeField]
    private Image rewardImage;
    [SerializeField]
    private TextMeshProUGUI countText;

    public WheelRewardType RewardType
    {
        get;
        set;
    }

    public int Count
    {
        get;
        set;
    }

    public Item Item
    {
        get;
        set;
    }

    public void SetParameters(WheelRewardType type, Item item)
    {
        Item = item;
        RewardType = type;

        item.OnBought += BoostsHandler.Instance.BoostsHandler_OnBought;

        rewardImage.sprite = item.Sprite;
        countText.gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        if (Item != null)
        {
            Item.OnBought -= BoostsHandler.Instance.BoostsHandler_OnBought;
        }
    }


    public void SetParameters(WheelRewardType type, int count)
    {
        Count = count;
        RewardType = type;

        switch (type)
        {
            case WheelRewardType.Coin:
                rewardImage.sprite = Resources.Load<Sprite>(WHEEL_SPRITES_PATH + "Coin");
                break;
        }

        countText.text = count.ToString();
    }


    public void GetPrize()
    {
        switch (RewardType)
        {
            case WheelRewardType.Coin:
                PlayerAttributes.PlayerProperties.Money += Count;
                break;
            case WheelRewardType.Dollars:
                break;
            case WheelRewardType.Gainer:
            case WheelRewardType.Protein:
                Item.Bought();
                break;
        }
    }
}
