using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Boost : MonoBehaviour
{
    public Item Properties { get; set; }
    public DateTime StartTime { get; set; }
    private TimeSpan nowTime;
    public TimeSpan NowTime
    {
        get { return nowTime; }
        set
        {
            nowTime = value;
            BoughtHandler.Save();
        }
    }

    public DateTime EndTime { get; set; }

    public Text text;
    public Text Text
    {
        get { return text = text == null ? gameObject.GetComponentInChildren<Text>() : text; }
    }

    [SerializeField]
    private Image image;
    public Image Image
    {
        get { return image = image == null ? gameObject.GetComponentInChildren<Image>() : image; }
    }

    public void CallTicks()
    {
        StartCoroutine(Ticks());
    }

    private IEnumerator Ticks()
    {
        NowTime = EndTime - StartTime;
        Text.text = NowTime.ToString("m:f");
        yield return new WaitForSeconds(1f);
    }
}
