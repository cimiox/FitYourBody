using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBehaviour : MonoBehaviour
{
    public Boost Boost { get; set; }

    [SerializeField]
    protected Text TimerText;

    [SerializeField]
    protected Image boostImage;

    public Image BoostImage { get { return boostImage; } } 

    protected virtual void Start()
    {

    }
}
