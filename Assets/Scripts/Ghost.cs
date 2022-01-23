using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed = 3f;
    public float damagePerSecond = 30f;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceFromPlayer = player.transform.position - transform.position;
        distanceFromPlayer = Vector3.ClampMagnitude(distanceFromPlayer, 1);
        transform.position += distanceFromPlayer * Time.deltaTime * speed;

        transform.forward = distanceFromPlayer;

    }
}
