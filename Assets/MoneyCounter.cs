using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        PlayerAttributes.Properties.OnMoneyChanged += Properties_OnMoneyChanged;
        Properties_OnMoneyChanged();
    }

    private void OnDisable()
    {
        PlayerAttributes.Properties.OnMoneyChanged -= Properties_OnMoneyChanged;
    }

    private void Properties_OnMoneyChanged()
    {
        moneyText.text = PlayerAttributes.PlayerProperties.Money.ToString();
    }
}
