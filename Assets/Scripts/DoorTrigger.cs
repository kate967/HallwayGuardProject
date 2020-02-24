using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject gC;
    public GameObject door;

    private GameController gameController;
    private bool hitDoor;

    void Start()
    {
        gameController = gC.GetComponent<GameController>();
    }

    void Update()
    {
        if(hitDoor)
        {
            gameController.DoorWasHit(door);
            hitDoor = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Door"))
        {
            hitDoor = true;
        }
    }
}
