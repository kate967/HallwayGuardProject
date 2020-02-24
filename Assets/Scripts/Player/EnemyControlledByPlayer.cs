using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlledByPlayer : MonoBehaviour
{
    public float speed = 7f;
    public float dashSpeed;
    public float thrust = 2f;
    public Vector3 offset;
    public Vector3 origCameraPos;
    public GameObject enemyCamera;
    public GameObject door;
    public GameObject inGameUI;
    public GameObject gC;

    private Rigidbody rb;
    private Animation anim;
    private Rigidbody doorRb;
    private GameController gameController;
    private InGameUI inGameUIContoller;
    private float timer;
    private float distance;

    /*NOTE
     *When the player is playing as the enemy
     *the actual enemy will be set to false
     *and this enemy will appear in the position of the actual enemy
     */

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        doorRb = door.GetComponent<Rigidbody>();
        gameController = gC.GetComponent<GameController>();
        inGameUIContoller = inGameUI.GetComponent<InGameUI>();

        distance = Vector3.Distance(transform.position, enemyCamera.transform.position);
        origCameraPos = enemyCamera.gameObject.transform.localPosition;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (InputManager.instance.player.GetButtonDown("Interact"))
        {
            inGameUIContoller.DetermineAction(4);
            gameController.BecomePlayer();
        }
        if(InputManager.instance.player.GetButtonDown("Dash"))
        {
            Dash();
        }

        AdjustCamera();

        if(rb.velocity.magnitude > 0)
        {
            anim["Take 001"].speed = 7.0f;
        }
        else if(rb.velocity.magnitude <= 0)
        {
            anim["Take 001"].speed = 0f;
        }
    }

    void Move()
    {
        Vector3 camDistance = transform.position - enemyCamera.transform.position;
        camDistance.y = 0;
        camDistance.Normalize();

        float mH = InputManager.instance.player.GetAxis("Horizontal");
        float mV = InputManager.instance.player.GetAxis("Vertical");

        Vector3 movement = (camDistance * mV + enemyCamera.transform.right * mH) * speed;

        rb.AddForce(movement * speed);
    }

    void Dash()
    {
        if (timer <= 0)
        {
            Vector3 dashMovement = transform.forward;
            rb.AddForce(dashMovement * dashSpeed);
            timer = 2f;
        }
    }

    void AdjustCamera()
    {
        RaycastHit hit;
        Ray backRay = new Ray((transform.position + offset), -((transform.position + offset) - enemyCamera.transform.position));

        Debug.DrawRay((transform.position + offset), -((transform.position + offset) - enemyCamera.transform.position), Color.green);

        if (Physics.Raycast(backRay, out hit, distance) && hit.collider.gameObject != enemyCamera)
        {
            enemyCamera.transform.position = hit.point;
        }
        else
        {
            enemyCamera.gameObject.transform.localPosition = origCameraPos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Door"))
        {
            doorRb.AddForce(door.transform.forward * thrust, ForceMode.Force);
        }
    }
}
