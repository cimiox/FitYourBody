using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMuscleMode : MonoBehaviour
{
    private void Start()
    {
        SportingGoodsItem.OnChooseMuscle += SportingGoodsItem_OnChooseMuscle;
        gameObject.SetActive(false);
    }

    private void SportingGoodsItem_OnChooseMuscle(System.Action actionAfterSelection)
    {
        gameObject.SetActive(true);
        //TODO: Wait to choose muscle
        //StartCoroutine();
    }
}
