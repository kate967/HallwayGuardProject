using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraController : MonoBehaviour
{
    public float rotationSpeed;
    public float adjustedNearClip;
    public float normalNearClip;
    public Transform enemyPlayer;

    private float mX;
    private float mY;

    void LateUpdate()
    {
        CameraController();
    }

    void CameraController()
    {
        mX += InputManager.instance.player.GetAxis("Mouse X") * rotationSpeed;
        mY -= InputManager.instance.player.GetAxis("Mouse Y") * rotationSpeed;
        mY = Mathf.Clamp(mY, -35, 60);

        enemyPlayer.rotation = Quaternion.Euler(0, mX, 0);
    }
}
