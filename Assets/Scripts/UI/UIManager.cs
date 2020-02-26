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
    public GameObject enemy;
    public GameObject enemyPlayer;
    public GameObject audioManager;
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
    private EnemyController enemyController;
    private EnemyControlledByPlayer enemyControlledByPlayer;
    private AuidoManager auidoManager; //i realize i spelled audio wrong lol
    private EventSystem eventSystem;

    void Awake()
    {
        eventSystem = myEventSystem.GetComponent<EventSystem>();
    }

    void Start()
    {
        gameController = gC.GetComponent<GameController>();
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
        enemyControlledByPlayer = enemyPlayer.GetComponent<EnemyControlledByPlayer>();
        auidoManager = audioManager.GetComponent<AuidoManager>();

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
            if (enemyPlayer.gameObject.activeSelf)
            {
                enemyControlledByPlayer.source.Stop();
                enemyControlledByPlayer.source2.Stop();
            }
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
        enemyController.source.Stop();

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

        enemyController.source.Stop();
        enemyControlledByPlayer.source.Stop();
        enemyControlledByPlayer.source2.Stop();
        playerController.source.Stop();
        playerController.source2.Stop();

        auidoManager.source.clip = auidoManager.won;
        auidoManager.source.volume = 0.2f;
        auidoManager.source.Play();

        gameController.wonGame = false;
        Time.timeScale = 0;
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

        enemyController.source.Stop();
        enemyControlledByPlayer.source.Stop();
        enemyControlledByPlayer.source2.Stop();
        playerController.source.Stop();
        playerController.source2.Stop();

        auidoManager.source.clip = auidoManager.lost;
        auidoManager.source.volume = 0.2f;
        auidoManager.source.Play();

        gameController.lostGame = false;
        Time.timeScale = 0;
    }
}
