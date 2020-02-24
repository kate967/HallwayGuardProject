using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject gC;

    private GameController gameController;

    void Start()
    {
        gameController = gC.GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameController.wonGame = true;
        }
    }
}
