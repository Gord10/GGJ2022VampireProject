using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public UiBar bloodBar, deliveranceBar;

    public void UpdateBloodBar(float currentValue, float maxValue)
    {
        bloodBar.SetTargetValue(currentValue / maxValue);
    }

    public void UpdateDeliveranceBar(float currentValue, float maxValue)
    {
        deliveranceBar.SetTargetValue(currentValue / maxValue);
    }

    public void ResetBars(float bloodValue, float deliveranceValue = 0f)
    {
        bloodBar.SetValueInstantly(bloodValue);
        deliveranceBar.SetValueInstantly(deliveranceValue);
    }


}
