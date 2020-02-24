using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Player player;
    public bool controller;
    public int doOneTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
        instance.doOneTime = 0;
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        Rewired.Controller lastControllerObj = ReInput.controllers.GetLastActiveController();
        if (lastControllerObj.type == ControllerType.Joystick)
        {
            controller = true;
        }
        else if (lastControllerObj.type == ControllerType.Keyboard)
        {
            controller = false;
        }
    }
}
