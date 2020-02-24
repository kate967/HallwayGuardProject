using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinAndLoseUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGameUI;
    public GameObject creditsMenu;
    public GameObject player;
    public GameObject gC;

    public void GoToMainMenu()
    {
        InputManager.instance.doOneTime = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
