using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Transform model;
    public Transform urnPoint;     
    private Rigidbody rigidbody;
    private GameManager gameManager;
    private Renderer renderer;
    private CharacterController characterController;
    private Animator animator;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

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
        Vector3 movement = new Vector2();
        movement.x = -Input.GetAxis("Horizontal");
        
        movement.z = -Input.GetAxis("Vertical");

        if(movement.x == 0 && movement.z == 0)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        animator.SetBool("isRunning", true);
        movement = Vector3.ClampMagnitude(movement, 1f);

        float angle = Mathf.Atan2(movement.z, -movement.x);
        angle *= Mathf.Rad2Deg;
        angle -= 90f;

        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        model.rotation = Quaternion.RotateTowards(model.rotation, targetRotation, Time.deltaTime * 360);

        movement.y = -10;
        characterController.Move(movement * Time.deltaTime * speed);
        //float angle = Mathf.Atan2(movement.x)
        //Quaternion targetModelRotation = Quaternion.

        //model.up = movement;
        //rigidbody.velocity = movement * speed;
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


    private void OnTriggerEnter(Collider other)
    {
        string collisionTag = other.gameObject.tag;
        if(collisionTag == "DeliveranceItem")
        {
            DeliveranceItem item = other.gameObject.GetComponent<DeliveranceItem>();
            gameManager.ReportDeliveranceItemCollection(item);
        }
        else if(collisionTag == "Coffin")
        {
            gameManager.ReportBringingDeliveranceItem();
        }
        else if(collisionTag == "Ghost")
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        string collisionTag = other.gameObject.tag;
        if (collisionTag == "Ghost")
        {
            Ghost ghost = other.gameObject.GetComponent<Ghost>();
            gameManager.ReportGhostTouch(ghost);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Villager")
        {
            Villager villager = hit.gameObject.GetComponent<Villager>();
            gameManager.ReportFeeding(villager);
            print("Hitting villager");
        }
    }

}
