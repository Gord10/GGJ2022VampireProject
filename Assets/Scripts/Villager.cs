using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public float speed = 4f;
    public float blood = 10f; //The blood that vampire will gain from feeding from the villager
    public float minDistanceToRunFromPlayer = 15f; //If the distance is longer than this value, don't run from the player

    private Rigidbody2D rigidbody2D;

    private Player player;
    private Renderer renderer;
    private GameManager gameManager;
    public enum State
    {
        RUNNING_FROM_PLAYER,
        IDLE
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();

        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 distanceFromPlayer = player.transform.position - transform.position;

        if(distanceFromPlayer.magnitude <= minDistanceToRunFromPlayer)
        {
            rigidbody2D.velocity = -distanceFromPlayer.normalized * speed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if(collisionTag == "Environment")
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = gameManager.GetSpawnPoint();
    }
}
