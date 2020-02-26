using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraController : MonoBehaviour
{
    public float rotationSpeed;
    public Transform enemyPlayer;

    private float mX;
    private float mY;

    void LateUpdate()
    {
        CameraController();
    }

    void Update()
    {
        if(Time.timeScale >= 1)
        {
            if (InputManager.instance.controller)
            {
                rotationSpeed = 7f;
            }
            if (!InputManager.instance.controller)
            {
                rotationSpeed = 20f;
            }
        }
        else
        {
            rotationSpeed = 0;
        }
    }

    void CameraController()
    {
        mX += InputManager.instance.player.GetAxis("Mouse X") * rotationSpeed;
        mY -= InputManager.instance.player.GetAxis("Mouse Y") * rotationSpeed;
        mY = Mathf.Clamp(mY, -35, 60);

        enemyPlayer.rotation = Quaternion.Euler(0, mX, 0);
    }
}
