using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public GameObject gC;
    public Transform checkForPlayer;
    public Transform[] points;
    public Transform[] hallwayPoints;
    public Vector3 targetPos;
    public LayerMask playerMask;

    public float distFromPlayer;

    private int destPoint = 0;
    private int newDestPoint = 0;
    private float timer;
    [SerializeField] private bool chasingPlayer = false;

    private NavMeshAgent agent;
    private PlayerController playerController;
    private GameController gameController;
    private Animation anim;
    
    void Start ()
    {
        playerController = player.GetComponent<PlayerController>();
        gameController = gC.GetComponent<GameController>();

        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animation>();
        
        anim["Take 001"].speed = 5.0f;

        agent.autoBraking = false;

        timer = 5;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(playerController.wasHit)
        {
            chasingPlayer = false;
            StartCoroutine(Wait(1));
        }
        if(!playerController.wasHit)
        {
            chasingPlayer = Physics.CheckSphere(checkForPlayer.position, distFromPlayer, playerMask);
        }
        if (chasingPlayer && timer < 1 && timer > 0)
        {
            timer = 5f;
            agent.isStopped = false;
            anim["Take 001"].speed = 5.0f;
            agent.speed = 4.5f;
            agent.acceleration = 120;
        }

        PathControl();
        DashControl();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void GoToNextHallwayPoint()
    {
        if (hallwayPoints.Length == 0)
            return;

        agent.destination = hallwayPoints[newDestPoint].position;

        newDestPoint = (newDestPoint + 1) % hallwayPoints.Length;
    }

    void PathControl()
    {
        targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (chasingPlayer && !playerController.underTable)
        {
            transform.LookAt(targetPos);
            agent.destination = player.transform.position;
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (!gameController.goToHallway)
            {
                GotoNextPoint();
            }
            if (gameController.goToHallway)
            {
                GoToNextHallwayPoint();
            }
        }
    }

    void DashControl()
    {

        if (!gameController.goToHallway && !chasingPlayer)
        {
            if (timer > 0 && timer < 5f)
            {
                agent.isStopped = false;
                anim["Take 001"].speed = 5.0f;
                agent.speed = 4.5f;
                agent.acceleration = 120;
            }
            if (timer > 0 && timer < 1)
            {
                agent.isStopped = true;
                anim["Take 001"].speed = 0f;
            }
            if (timer > 0 && timer < 0.25f)
            {
                agent.isStopped = false;
                anim["Take 001"].speed = 5.0f;
                agent.speed = 35;
                agent.acceleration = 500;
            }
            if (timer <= 0)
            {
                timer = 5f;
            }
        }
        if(gameController.goToHallway && chasingPlayer)
        {
            agent.isStopped = false;
            anim["Take 001"].speed = 5.0f;
            agent.speed = 4.5f;
            agent.acceleration = 120;
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return (new WaitForSeconds(time));
        playerController.wasHit = false;
    }

}
