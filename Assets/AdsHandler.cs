using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsHandler : MonoBehaviour, IRewardedVideoAdListener
{
    [SerializeField]
    private Button rewardedVideoButton;

    public static List<Action> OnRewardedVideoFinishedCallbacks { get; } = new List<Action>();
    public static List<Action> OnRewardedVideoNotFinishedCallbacks { get; } = new List<Action>();

    public void onRewardedVideoClosed(bool finished)
    {
        if (finished)
        {
            foreach (var item in OnRewardedVideoFinishedCallbacks)
            {
                item.Invoke();
            }
        }
        else
        {
            foreach (var item in OnRewardedVideoNotFinishedCallbacks)
            {
                item.Invoke();
            }
        }

        OnRewardedVideoFinishedCallbacks.Clear();
        OnRewardedVideoNotFinishedCallbacks.Clear();
    }


    public void onRewardedVideoFailedToLoad()
    {

    }

    public void onRewardedVideoFinished(int amount, string name)
    {
        PlayerAttributes.PlayerProperties.Money += 100;
    }

    public void onRewardedVideoLoaded()
    {
    }

    public void onRewardedVideoShown()
    {
    }

    private void Awake()
    {
        Appodeal.disableNetwork("startapp");
        Appodeal.disableNetwork("mmedia");

        Appodeal.initialize("78f92713244b3d1761a1423d7da6c36b6bccac941c25147b",
            Appodeal.BANNER_BOTTOM | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);
        Appodeal.setRewardedVideoCallbacks(this);

        rewardedVideoButton.onClick.AddListener(() =>
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            rewardedVideoButton.gameObject.SetActive(false);
        });

        WindowsHandler.Instance.Windows.CollectionChanged += Windows_CollectionChanged;
    }

    private void Windows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (WindowsHandler.Instance.Windows.Count > 0)
        {
            Appodeal.hide(Appodeal.BANNER_BOTTOM);
        }
        else
        {
            Appodeal.show(Appodeal.BANNER_BOTTOM);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => Appodeal.isLoaded(Appodeal.BANNER_BOTTOM));
        Appodeal.show(Appodeal.BANNER_BOTTOM);

        yield return new WaitUntil(() => Appodeal.isLoaded(Appodeal.INTERSTITIAL));
        Appodeal.show(Appodeal.INTERSTITIAL);

        while (true)
        {
            yield return new WaitUntil(() => Appodeal.isLoaded(Appodeal.REWARDED_VIDEO));
            rewardedVideoButton.gameObject.SetActive(true);
            yield return new WaitUntil(() => !rewardedVideoButton.gameObject.activeSelf);
        }
    }
}
