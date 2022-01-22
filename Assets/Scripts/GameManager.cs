using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float blood = 100f;
    public float deliverance = 10f;
    public float bloodLoseSpeed = 2f;

    private GameUi gameUi;
    private float maxBlood; //Assigned at Awake by "blood" variable

    private void Awake()
    {
        gameUi = FindObjectOfType<GameUi>();
        maxBlood = blood;

        //gameUi.UpdateDeliveranceBar(deliverance, )
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blood -= bloodLoseSpeed * Time.deltaTime;
        gameUi.UpdateBloodBar(blood, maxBlood);


    }
}
