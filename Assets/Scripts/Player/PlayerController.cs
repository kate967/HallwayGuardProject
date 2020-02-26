using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /***Player Attributes**/
    public int maxHealth = 6;
    public float speed = 12f;
    public float jumpHeight;
    public float jumpTime;
    public float gravity = -19.62f;
    public float groundDistance = 0.4f;
    public float colliderDistance = 1f;
    public bool underTable;
    public bool wasHit;
    public bool uiButtonWasPressed;
    
    /***Player References**/
    public CharacterController controller;
    public LookController lookController;
    public GameObject inGameUI;
    public GameObject gC;
    public Transform groundChecker;
    public Transform colliderChecker;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    public LayerMask remoteMask;
    public AudioClip hitClip;

    /***Controller Vibration***/
    public int motorIndex;
    public float motorLevel;
    public float duration;

    /***Private Variables**/
    private GameController gameController;
    private InGameUI inGameUIController;
    private Vector3 velocity;
    public AudioSource source;
    public AudioSource source2;
    private int currHealth;
    private float jumpTimeCounter;
    private float timer;
    private bool isGrounded;
    private bool isJumping;
    private bool isEnemy;
    private bool isRemote;

    void Start()
    {
        gameController = gC.GetComponent<GameController>();
        inGameUIController = inGameUI.GetComponent<InGameUI>();
        source = this.GetComponent<AudioSource>();

        currHealth = maxHealth;
    }
    void Update()
    {
        ColliderChecks();
        GroundCheck();
        
        if(!uiButtonWasPressed)
        {
            Jump();
        }

        if (currHealth <= 0)
        {
            gameController.lostGame = true;
            currHealth = 9999;
        }
        

    }

    void FixedUpdate()
    {
        Move();
        if (uiButtonWasPressed && InputManager.instance.player.GetButton("Jump"))
        {
            uiButtonWasPressed = false;
        }
    }

    void Move()
    {
        float x = InputManager.instance.player.GetAxis("Horizontal");
        float z = InputManager.instance.player.GetAxis("Vertical");

        Vector3 movement = (transform.right * x + transform.forward * z).normalized;
        controller.Move(movement * speed * Time.deltaTime);

        if (movement.magnitude > Vector3.zero.magnitude && !source.isPlaying && !isJumping)
        {
            if(InputManager.instance.player.GetButtonDown("Jump") || isJumping || InputManager.instance.player.GetButtonDown("Jump") || InputManager.instance.player.GetButton("Jump") || !isGrounded)
            {
                source.Stop();
            }
            else
            {
                source.Play();
            }
        }
        else if (movement.magnitude <= Vector3.zero.magnitude || isJumping || InputManager.instance.player.GetButtonDown("Jump") || InputManager.instance.player.GetButton("Jump") || !isGrounded)
        {
            source.Stop();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        if (InputManager.instance.player.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (InputManager.instance.player.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (InputManager.instance.player.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    void UpdateHealth()
    {
        source2.PlayOneShot(hitClip);

        currHealth--;
        inGameUIController.UpdateVisualHealth(currHealth);
    }

    void ColliderChecks()
    {
        CollideWithEnemy();
        CollideWithRemote();
    }

    void CollideWithEnemy()
    {
        /***Checks for Enemy Collision***/
        timer -= Time.deltaTime;

        isEnemy = Physics.CheckSphere(colliderChecker.position, colliderDistance, enemyMask);

        if (isEnemy && timer <= 0)
        {
            wasHit = true;
            inGameUIController.TookEnemyHit();
            if(currHealth > 1)
                StartCoroutine(lookController.CameraShake(.20f, .4f));
            InputManager.instance.player.SetVibration(motorIndex, motorLevel, duration);
            
            timer = 3f;

            UpdateHealth();
        }
        if (timer <= 0)
        {
            inGameUIController.RestoreOrigColor();
        }
    }

    void CollideWithRemote()
    {
        /***Checks for Remote Control***/
        isRemote = Physics.CheckSphere(colliderChecker.position, colliderDistance, remoteMask);

        if (isRemote)
        {
            inGameUIController.DetermineAction(1);

            if (isRemote && InputManager.instance.player.GetButtonDown("Interact"))
            {
                inGameUIController.DetermineAction(3);

                gameController.BecomeEnemy();
            }
        }
        else
        {
            inGameUIController.DetermineAction(2);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Table"))
        {
            underTable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Table"))
        {
            underTable = false;
        }
    }
}
