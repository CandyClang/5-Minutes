using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float baseSpeed;
    public float gravity = -9.81f;
    public float jumpForce;
    public CharacterController controller;
    public Transform cam;
    public bool isGrounded;
    public bool canJump;
    //public float jumpdelay = .58f;
    public float jumpDelay = 0;

    public GameObject questionMark;
    public bool canInteract;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private Vector3 rotateDirection;
    public float gravityScale;

    public Collider[] attackHitboxes;

    public float turnSmoothtime = 0.1f;
    float turnSmoothSensitivity;
    private Quaternion rotate;

    public Animator animator;

    private bool triggeredAlready = false;

    private GameStateManager manager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        baseSpeed = moveSpeed;

        GameObject mgr = GameObject.Find("Game State Manager");

        if (transform.childCount > 0) animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        if (mgr != null) manager = mgr.GetComponent<GameStateManager>();

        moveSpeed = 10;
        canJump = true;
        questionMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (canInteract)
        {
            questionMark.SetActive(true);
        } else
        {
            questionMark.SetActive(false);
        }
        /*
        if (animator.GetBool("Carrying"))
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Human").GetComponent<Transform>().position, transform.position) < 30f)
            {
                GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
                foreach(GameObject interactable in interactables)
                {
                    if (interactable.GetComponent<InteractableObject>().objectType == InteractableObject.ObjectType.TOY)
                    {
                        interactable.GetComponent<InteractableObject>().DropToy();
                    }
                }
            }
        }
        */

    }

    private void Move()
    {

        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else animator.SetFloat("Speed", 0);

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothSensitivity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }


        //Gravity
        velocity.y += gravity * Time.deltaTime * 2f;
        controller.Move(velocity * Time.deltaTime);

        //Jumping
       
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (canJump)
        {
            if (controller.isGrounded)
            {
                if (!triggeredAlready)
                {
                    if (Input.GetButton("Jump"))
                    {
                        animator.SetTrigger("Jump");
                        triggeredAlready = true;
                        Invoke("Jump", jumpDelay);
                    }
                }
            }
        }
    }

    //Jump
    void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * -10 * gravity);
        triggeredAlready = false;
    }

    //collision with objects


    //Change player speed when touching blue / red orbs
    private void ChangeSpeed(float decider)
    {
        if (decider == 1)
        {
            moveSpeed = moveSpeed * 2;
            Invoke("ResetPlayerStats", 10.0f);
        }

        if (decider == 2)
        {
            moveSpeed = moveSpeed / 2;
            Invoke("ResetPlayerStats", 10.0f);
        }
    }

    private void ResetPlayerStats()
    {
        moveSpeed = baseSpeed;
    }

}