using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsAndCreditsUI : MonoBehaviour
{

    public void GoBack()
    {
        InputManager.instance.doOneTime = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
