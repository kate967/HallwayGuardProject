using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject player;
    public GameObject enemy;
    public GameObject enemyPlayer;

    private PlayerController playerController;
    private EnemyController enemyController;
    private EnemyControlledByPlayer enemyControlledByPlayer;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
        enemyControlledByPlayer = enemyPlayer.GetComponent<EnemyControlledByPlayer>();
    }

    public void ResumeGame()
    {
        InputManager.instance.doOneTime = 0;
        if(enemy.gameObject.activeSelf)
        {
            enemyController.source.Play();
        }
        if(enemyPlayer.gameObject.activeSelf)
        {
            enemyControlledByPlayer.source.Play();
            enemyControlledByPlayer.source2.Play();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

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
