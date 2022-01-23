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
    public LayerMask groundLayerMask;

    private GameUi gameUi;
    private int collectedDeliveranceItemAmount = 0;
    private int totalDeliveranceItemAmount = 0; //Assigned at the Awake
    private bool isPlayerCollectingItem = false;
    private Player player;
    private DeliveranceItem currentDeliveranceItem;

    private void Awake()
    {
        gameUi = FindObjectOfType<GameUi>();
        totalDeliveranceItemAmount = FindObjectsOfType<DeliveranceItem>().Length;
        player = FindObjectOfType<Player>();
        gameUi.ResetBars(blood / maxBlood);
        gameUi.UpdateDeliveranceBar(deliverance, 100);
    }

    public void ReportBringingDeliveranceItem()
    {
        if(!isPlayerCollectingItem)
        {
            return;
        }

        isPlayerCollectingItem = false;
        collectedDeliveranceItemAmount++;
        deliverance += currentDeliveranceItem.deliverance;

        gameUi.UpdateDeliveranceBar(deliverance, 100);
        gameUi.SetDeliveranceIconActivity(false);

        currentDeliveranceItem.transform.SetParent(null);
        currentDeliveranceItem.transform.position = GetSpawnPoint();
        
        //Destroy(currentDeliveranceItem.gameObject);
        if (deliverance >= 100f)
        {
            SceneManager.LoadScene("Good Ending");
        }
    }

    public void ReportGhostTouch(Ghost ghost)
    {
        blood -= ghost.damagePerSecond * Time.fixedDeltaTime;
    }

    public void ReportDeliveranceItemCollection(DeliveranceItem item)
    {
        if(isPlayerCollectingItem)
        {
            return;
        }

        isPlayerCollectingItem = true;
        item.transform.SetParent(player.urnPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        currentDeliveranceItem = item;
        //item.gameObject.SetActive(false);
        gameUi.SetDeliveranceIconActivity(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 GetSpawnPoint()
    {
        float x = Random.Range(minGameX, maxGameX);
        float z = Random.Range(minGameY, maxGameY);

        Vector3 point = new Vector3(x, 0, z);
        Ray ray = new Ray(point, Vector3.up * 10000f);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000, groundLayerMask)) 
        {
            //print("sdsd");
            return hit.point;
        }

        ray.direction = Vector3.down * 10000;

        if (Physics.Raycast(ray, out hit, 1000, groundLayerMask))
        {
            //print("dfdf");
            return hit.point;
        }

        //print("Random point: " + point.ToString());
        return point;
    }

    public void ReportFeeding(Villager villager)
    {
        blood += villager.blood;
        deliverance -= villager.deliveranceLose;
        gameUi.UpdateDeliveranceBar(deliverance, 100f);


        if(deliverance <= 0f)
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
