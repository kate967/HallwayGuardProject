using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float speed;
    public bool rotateX;
    public bool rotateZ;
    void FixedUpdate()
    {
        if(rotateX)
        {
            transform.Rotate(new Vector3(Time.deltaTime * speed, 0, 0));
        }
        if (rotateZ)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        }
    }
}
