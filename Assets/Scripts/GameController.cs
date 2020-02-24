using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject squirrelModel;
    public GameObject remote;
    public GameObject playerAsEnemy;
    public GameObject realEnemy;
    public GameObject door;
    public GameObject doorTrigger;
    public GameObject hallwayPlane;
    public bool goToHallway;
    public bool lostGame = false;
    public bool wonGame = false;

    void Start()
    {
        //on level start
        player.SetActive(false);
        playerAsEnemy.SetActive(false);
        squirrelModel.SetActive(false);

        hallwayPlane.SetActive(false);
    }

    public void BecomeEnemy()
    {
        player.SetActive(false);
        remote.SetActive(false);

        playerAsEnemy.SetActive(true);
        playerAsEnemy.transform.position = realEnemy.transform.position;

        squirrelModel.SetActive(true);
        squirrelModel.transform.position = player.transform.position;

        realEnemy.SetActive(false);
    }

    public void BecomePlayer()
    {
        player.SetActive(true);
        remote.SetActive(true);
        realEnemy.SetActive(true);

        realEnemy.transform.position = playerAsEnemy.transform.position;

        playerAsEnemy.SetActive(false);
        squirrelModel.SetActive(false);
    }

    public void DoorWasHit(GameObject door)
    {
        StartCoroutine(Wait(3, door));
        hallwayPlane.SetActive(true);
        goToHallway = true;
    }

    private IEnumerator Wait(float time, GameObject door)
    {
        yield return(new WaitForSeconds(time));
        door.SetActive(false);
        doorTrigger.SetActive(false);
    }
}
