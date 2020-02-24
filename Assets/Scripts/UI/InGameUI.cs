using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject player;
    public GameObject pressAction;
    public GameObject exitAction;
    public GameObject dashAction;
    public Image visualHealthFull;
    public Image visualHealthEmpty;
    public Image panel;
    public Text actionText;
    public Text exitText;
    public Text dashText;

    private PlayerController playerController;
    private Color origColor;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        origColor = panel.color;

        pressAction.SetActive(false);
        exitAction.SetActive(false);
        dashAction.SetActive(false);
    }

    void Update()
    {
        GetInputType();
    }

    void GetInputType()
    {
        if (InputManager.instance.controller == true)
        {
            actionText.text = "Press X to Interact";
            exitText.text = "Press X to Exit";
            dashText.text = "Press A to Dash";
        }
        if (InputManager.instance.controller == false)
        {
            actionText.text = "Press E to Interact";
            exitText.text = "Press E to Exit";
            dashText.text = "Press Q to Dash";
        }
    }

    public void UpdateVisualHealth(int currHealth)
    {
        visualHealthFull.fillAmount = ((float)currHealth / playerController.maxHealth);
    }

    public void TookEnemyHit()
    {
        panel.color = new Color(255, 0, 0, .25f);
    }

    public void RestoreOrigColor()
    {
        panel.color = origColor;
    }

    public void DetermineAction(int action)
    {
        if(action == 1)
        {
            pressAction.SetActive(true);
            return;
        }
        else if(action == 2)
        {
            pressAction.SetActive(false);
            return;
        }
        else if(action == 3)
        {
            pressAction.SetActive(false);
            visualHealthFull.gameObject.SetActive(false);
            visualHealthEmpty.gameObject.SetActive(false);
            exitAction.SetActive(true);
            dashAction.SetActive(true);
            return;
        }
        else if(action == 4)
        {
            exitAction.SetActive(false);
            dashAction.SetActive(false);
            visualHealthFull.gameObject.SetActive(true);
            visualHealthEmpty.gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
