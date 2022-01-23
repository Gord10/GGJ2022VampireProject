using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public UiBar bloodBar, deliveranceBar;
    public Slider deliveranceSlider;
    public GameObject deliveranceIcon;
    public Text urnsText;

    private void Awake()
    {
        SetDeliveranceIconActivity(false);
    }

    public void SetDeliveranceIconActivity(bool isActive)
    {
        deliveranceIcon.SetActive(isActive);
    }

    public void UpdateBloodBar(float currentValue, float maxValue)
    {
        bloodBar.SetTargetValue(currentValue / maxValue);
    }

    public void UpdateDeliveranceBar(float currentValue, float maxValue)
    {
        //deliveranceBar.SetTargetValue(currentValue / maxValue);
        //urnsText.text = "Goblets: " + currentValue + "/" + maxValue;
        deliveranceSlider.value = currentValue / maxValue;
    }

    public void ResetBars(float bloodValue, float deliveranceValue = 0f)
    {
        bloodBar.SetValueInstantly(bloodValue);
        deliveranceBar.SetValueInstantly(deliveranceValue);
    }


}
