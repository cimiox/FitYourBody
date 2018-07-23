using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppodealAds.Unity.Api;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfFortune : MonoBehaviour
{
    private const string GAINER_RESOURCES_PATH = "Shop/GeinerShop";
    private const string PROTEIN_RESOURCES_PATH = "Shop/ProteinShop";

    [SerializeField]
    private WheelOfFortunePrizesDB prizesDB;

    [SerializeField]
    private GameObject separator;

    [SerializeField]
    private GameObject wheelPrizePrefab;

    [SerializeField]
    private Button startWheelButton;
    [SerializeField]
    private Button startWheelByAdButton;

    [SerializeField]
    private GameObject objectForRotation;
    public GameObject ObjectForRotation
    {
        get { return objectForRotation; }
        set { objectForRotation = value; }
    }

    [SerializeField]
    private GameObject arrow;
    public GameObject Arrow
    {
        get { return arrow; }
        set { arrow = value; }
    }

    private float RotationTime { get; set; } = 5;
    private float DeltaTime { get; set; }
    private float WheelSpeed { get; set; }
    private WheelStates WheelState { get; set; } = WheelStates.Idle;

    public Quaternion StartPosition { get; set; } = Quaternion.identity;

    [SerializeField]
    private List<Prize> prizesStack;


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => PlayerAttributes.PlayerProperties != null);
        IntializeWheel();
    }


    private void Update()
    {
        switch (WheelState)
        {
            case WheelStates.Rotation:
                RotateWheel();
                break;
            case WheelStates.Stop:
                StopWheel();
                break;
            case WheelStates.Idle:
                return;
        }
    }


    private void IntializeWheel()
    {
        foreach (var item in prizesStack)
        {
            switch (item.Type)
            {
                case WheelRewardType.Coin:
                    item.Reward.SetParameters(item.Type, item.Count);
                    break;
                case WheelRewardType.Gainer:
                    var gainerItems = JsonConvert.DeserializeObject<List<SportNutritionItem>>(Resources.Load<TextAsset>(GAINER_RESOURCES_PATH).text);

                    item.Reward.SetParameters(item.Type, GetItemByLevel(gainerItems, item.Count));
                    break;
                case WheelRewardType.Protein:
                    var proteinItems = JsonConvert.DeserializeObject<List<SportNutritionItem>>(Resources.Load<TextAsset>(PROTEIN_RESOURCES_PATH).text);

                    item.Reward.SetParameters(item.Type, GetItemByLevel(proteinItems, item.Count));
                    break;
            }
        }
    }


    public void StartWheel()
    {
        WheelState = WheelStates.Rotation;
        WheelSpeed = UnityEngine.Random.Range(1.0f, 3.0f);
        startWheelButton.gameObject.SetActive(false);
    }


    public void StartWheelByAd()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            startWheelByAdButton.gameObject.SetActive(false);
            AdsHandler.OnRewardedVideoFinishedCallbacks.Add(StartWheel);
            AdsHandler.OnRewardedVideoNotFinishedCallbacks.Add(() =>
            {
                if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    startWheelByAdButton.gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            });

            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }


    private void RotateWheel()
    {
        DeltaTime += Time.deltaTime;
        ObjectForRotation.transform.Rotate(new Vector3(0, 0, WheelSpeed * DeltaTime));

        if (DeltaTime >= RotationTime)
        {
            WheelState = WheelStates.Stop;
        }
    }


    private void StopWheel()
    {
        DeltaTime -= Time.deltaTime;
        ObjectForRotation.transform.Rotate(new Vector3(0, 0, WheelSpeed * DeltaTime));

        if (DeltaTime <= 0)
        {
            WheelState = WheelStates.Idle;
            GivePrize();

            if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            {
                startWheelByAdButton.gameObject.SetActive(true);
            }
            else
            {
                StartCoroutine(WaitAndClose());
            }
        }
    }


    private IEnumerator WaitAndClose()
    {
        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);
    }


    private void GivePrize()
    {
        var prize = prizesStack[0];

        for (int i = 0; i < prizesStack.Count; i++)
        {
            if (Vector2.Distance(prizesStack[i].Reward.transform.position, Arrow.transform.position)
                < Vector2.Distance(prize.Reward.transform.position, Arrow.transform.position))
            {
                prize = prizesStack[i];
            }
        }

        prize.Reward.GetPrize();
    }


    public Item GetItemByLevel(List<SportNutritionItem> items, int level)
    {
        items.Sort((x, y) => x.Level.CompareTo(y.Level));

        Item currentItem = null;
        int currentItemLevel;
        IEnumerable<Item> itemsByLevel;

        switch (level)
        {
            case -1:

                itemsByLevel = items.Where(x => x.Level < PlayerAttributes.PlayerProperties.Level);
                if (itemsByLevel.Count() > 0)
                {
                    currentItemLevel = itemsByLevel.Max(x => x.Level);
                    currentItem = items.FirstOrDefault(x => x.Level == currentItemLevel);
                }

                if (currentItem == null)
                {
                    currentItem = items.First();
                }

                return currentItem;
            case 0:
                itemsByLevel = items.Where(x => x.Level == PlayerAttributes.PlayerProperties.Level);
                if (itemsByLevel.Count() > 0)
                {
                    currentItemLevel = itemsByLevel.Max(x => x.Level);
                    currentItem = items.FirstOrDefault(x => x.Level == currentItemLevel);
                }

                if (currentItem == null)
                {
                    if (items.Last().Level < PlayerAttributes.PlayerProperties.Level)
                    {
                        currentItem = items.Last();
                    }
                    else
                    {
                        currentItem = items.Where(x => x.Level >= PlayerAttributes.PlayerProperties.Level).First();
                    }
                }

                return currentItem;
            case 1:
                itemsByLevel = items.Where(x => x.Level > PlayerAttributes.PlayerProperties.Level);
                if (itemsByLevel.Count() > 0)
                {
                    currentItemLevel = itemsByLevel.Min(x => x.Level);
                    currentItem = items.FirstOrDefault(x => x.Level == currentItemLevel);
                }

                if (currentItem == null)
                {
                    currentItem = items.Last();
                }

                return currentItem;
            default:
                return items.First();
        }
    }

    private enum WheelStates
    {
        Rotation,
        Stop,
        Idle
    }

    [Serializable]
    public class Prize
    {
        [SerializeField]
        private WheelRewardType type;
        public WheelRewardType Type
        {
            get { return type; }
            set { type = value; }
        }

        [SerializeField]
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        [SerializeField]
        private WheelReward reward;
        public WheelReward Reward
        {
            get { return reward; }
            set { reward = value; }
        }
    }
}
