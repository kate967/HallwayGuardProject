using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject inGameUI;
    public GameObject backgroundMenu;
    public GameObject player;

    public void StartGame()
    {
        backgroundMenu.SetActive(false);
        inGameUI.SetActive(true);
        player.SetActive(true);
        
        InputManager.instance.doOneTime = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        
        this.gameObject.SetActive(false);
    }

    public void OpenControls()
    {
        controlsMenu.SetActive(true);
        this.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        this.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
