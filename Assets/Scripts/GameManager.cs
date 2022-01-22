using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float blood = 50f;
    public float maxBlood = 100f;

    public float deliverance = 10f;
    public float bloodLoseSpeed = 2f;

    public float minGameX, maxGameX; //Objects will spawn within this X range
    public float minGameY, maxGameY; //Objects will spawn within this Y range


    private GameUi gameUi;
    private int collectedDeliveranceItemAmount = 0;
    private int totalDeliveranceItemAmount = 0; //Assigned at the Awake

    private void Awake()
    {
        gameUi = FindObjectOfType<GameUi>();
        totalDeliveranceItemAmount = FindObjectsOfType<DeliveranceItem>().Length;
        gameUi.ResetBars(blood / maxBlood);
    }

    public void ReportDeliveranceItemCollection()
    {
        collectedDeliveranceItemAmount++;
        gameUi.UpdateDeliveranceBar(collectedDeliveranceItemAmount, totalDeliveranceItemAmount);

        if(collectedDeliveranceItemAmount == totalDeliveranceItemAmount)
        {
            SceneManager.LoadScene("Good Ending");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 GetSpawnPoint()
    {
        float x = Random.Range(minGameX, maxGameX);
        float y = Random.Range(minGameY, maxGameY);
        return new Vector2(x, y);
    }

    public void ReportFeeding(Villager villager)
    {
        blood += villager.blood;

        if(blood >= maxBlood)
        {
            SceneManager.LoadScene("Bad Ending");
            return;
        }

        blood = Mathf.Clamp(blood, 0, maxBlood);
        villager.Respawn();
    }


    // Update is called once per frame
    void Update()
    {
        blood -= bloodLoseSpeed * Time.deltaTime;

        if(blood <= 0)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        gameUi.UpdateBloodBar(blood, maxBlood);

#if UNITY_STANDALONE
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }


}
