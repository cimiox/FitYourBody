﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelText : MonoBehaviour
{
    void Start()
    {
        PlayerAttributes.OnLevelChanged += PlayerAttributes_OnLevelChanged;
        GetComponent<Text>().text = string.Format("Level: {0}", PlayerAttributes.Level);
    }

    private void PlayerAttributes_OnLevelChanged()
    {
        GetComponent<Text>().text = string.Format("Level: {0}", PlayerAttributes.Level);
    }
}
