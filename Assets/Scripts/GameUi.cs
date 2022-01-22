using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Image bloodBar, deliveranceBar;

    public void UpdateBloodBar(float currentValue, float maxValue)
    {
        bloodBar.fillAmount = currentValue / maxValue;
    }

    public void UpdateDeliveranceBar(float currentValue, float maxValue)
    {
        deliveranceBar.fillAmount = currentValue / maxValue;
    }

}
