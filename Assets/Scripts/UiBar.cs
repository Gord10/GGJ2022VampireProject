using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    private Image barImage;
    private float targetValue;

    private void Awake()
    {
        barImage = GetComponent<Image>();
        targetValue = barImage.fillAmount;
    }

    public void SetTargetValue(float targetValue)
    {
        this.targetValue = targetValue;
    }

    public void SetValueInstantly(float value)
    {
        if(barImage == null)
        {
            barImage = GetComponent<Image>();
        }

        barImage.fillAmount = value;
        targetValue = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = Mathf.MoveTowards(barImage.fillAmount, targetValue, Time.deltaTime);
    }
}
