using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    /***Menus***/
    public GameObject mainMenu;
    public GameObject backgroundMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject pauseMenu;
    public GameObject inGameUI;
    public GameObject winMenu;
    public GameObject loseMenu;

    /***References***/
    public GameObject gC;
    public GameObject player;
    public GameObject myEventSystem;

    /***Buttons***/
    public GameObject startButton;
    public GameObject resumeButton;
    public GameObject goBackButtonControls;
    public GameObject goBackButtonCredits;
    public GameObject winMMButton;
    public GameObject loseMMButton;

    /***Private Variables**/
    private GameController gameController;
    private PlayerController playerController;
    private EventSystem eventSystem;

    void Awake()
    {
        eventSystem = myEventSystem.GetComponent<EventSystem>();
    }

    void Start()
    {
        gameController = gC.GetComponent<GameController>();
        playerController = player.GetComponent<PlayerController>();

        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (InputManager.instance.controller == true)
        {
            eventSystem.SetSelectedGameObject(startButton);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        mainMenu.SetActive(true);
        backgroundMenu.SetActive(true);

        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameUI.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        Time.timeScale = 0;
    }

    void Update()
    {
        if(InputManager.instance.player.GetButtonDown("Pause") && !pauseMenu.activeSelf && inGameUI.activeSelf && !mainMenu.activeSelf && !winMenu.activeSelf && !loseMenu.activeSelf)
        {
            PauseGame();
            eventSystem.SetSelectedGameObject(null);
        }

        if(mainMenu.activeSelf)
        {
            MainMenuControl();
            if(InputManager.instance.controller == true)
            {
                playerController.uiButtonWasPressed = true;
            }
        }
        if (pauseMenu.activeSelf)
        {
            PauseMenuControl();
        }
        if (controlsMenu.activeSelf)
        {
            ControlsMenuControl();
        }
        if (creditsMenu.activeSelf)
        {
            CreditsMenuControl();
        }
        if(inGameUI.activeSelf && !pauseMenu.activeSelf && !winMenu.activeSelf && !loseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        if (gameController.wonGame && !pauseMenu.activeSelf)
        {
            WonGameUIControl();
        }
        if (gameController.lostGame && !pauseMenu.activeSelf)
        {
            LostGameUIControl();
        }
    }

    void MainMenuControl()
    {
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        inGameUI.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InputManager.instance.doOneTime = 0;
        }
        if (InputManager.instance.controller == true)
        {
            if (InputManager.instance.doOneTime == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                eventSystem.SetSelectedGameObject(startButton);
                InputManager.instance.doOneTime++;
                return;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void PauseMenuControl()
    {
        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InputManager.instance.doOneTime = 0;
        }
        if (InputManager.instance.controller == true)
        {
            if (InputManager.instance.doOneTime == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                eventSystem.SetSelectedGameObject(resumeButton);
                InputManager.instance.doOneTime++;
                return;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerController.uiButtonWasPressed = true;
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);

        eventSystem.SetSelectedGameObject(resumeButton);

        Time.timeScale = 0;
    }

    void ControlsMenuControl()
    {
        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (InputManager.instance.controller == true)
        {
            eventSystem.SetSelectedGameObject(goBackButtonControls);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void CreditsMenuControl()
    {
        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (InputManager.instance.controller == true)
        {
            eventSystem.SetSelectedGameObject(goBackButtonCredits);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void WonGameUIControl()
    {
        winMenu.SetActive(true);
        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (InputManager.instance.controller == true)
        {
            eventSystem.SetSelectedGameObject(winMMButton);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Time.timeScale = 0;

        gameController.wonGame = false;
    }

    void LostGameUIControl()
    {
        loseMenu.SetActive(true);
        if (InputManager.instance.controller == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (InputManager.instance.controller == true)
        {
            eventSystem.SetSelectedGameObject(loseMMButton);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        gameController.lostGame = false;
        Time.timeScale = 0;
    }
}
