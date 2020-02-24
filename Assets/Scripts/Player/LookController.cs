using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Update()
    {
        LookAround();
        
        if(InputManager.instance.controller)
        {
            mouseSensitivity = 300f;
        }
        if(!InputManager.instance.controller)
        {
            mouseSensitivity = 1600f;
        }
    }

    void LookAround()
    {
        float mouseX = InputManager.instance.player.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = InputManager.instance.player.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 origPos = transform.localPosition;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(x, y, origPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = origPos;
    }
}
