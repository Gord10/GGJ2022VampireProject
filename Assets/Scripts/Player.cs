using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;


    private Rigidbody2D rigidbody;
    private GameManager gameManager;
    private Renderer renderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();

        gameManager = FindObjectOfType<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1f);
        rigidbody.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        if(collisionTag == "Villager")
        {
            Villager villager = collision.gameObject.GetComponent<Villager>();
            gameManager.ReportFeeding(villager);
        }
    }
}
