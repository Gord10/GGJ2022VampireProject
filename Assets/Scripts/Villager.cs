using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public float speed = 4f;
    public float blood = 10f; //The blood that vampire will gain from feeding from the villager
    public float minDistanceToRunFromPlayer = 15f; //If the distance is longer than this value, don't run from the player
    public float deliveranceLose = 20f;
    private Rigidbody2D rigidbody2D;
    private CharacterController characterController;
   
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
        characterController = GetComponent<CharacterController>();
        renderer = GetComponent<Renderer>();

        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceFromPlayer = transform.position - player.transform.position;
        distanceFromPlayer.y = 0;
        distanceFromPlayer.Normalize();
        distanceFromPlayer.y = -10;

        float angle = Mathf.Atan2(-distanceFromPlayer.z, distanceFromPlayer.x);
        angle *= Mathf.Rad2Deg;
        angle -= 90f;

        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 360);

        if (distanceFromPlayer.magnitude <= minDistanceToRunFromPlayer)
        {
            characterController.Move(distanceFromPlayer * speed * Time.deltaTime);
            //rigidbody2D.velocity = -distanceFromPlayer.normalized * speed;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string collisionTag = hit.gameObject.tag;
        if (collisionTag == "Environment")
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = gameManager.GetSpawnPoint();
        characterController.enabled = true;
        //print("Respawn");
    }
}
