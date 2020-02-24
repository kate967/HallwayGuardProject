using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform positionA;
    public Transform positionB;
    public float speed;

    Vector3 nextPos;

    void Start()
    {
        nextPos = positionA.position;
    }

    void Update()
    {
        if(transform.position == positionA.position)
        {
            nextPos = positionB.position;
        }
        if(transform.position == positionB.position)
        {
            nextPos = positionA.position;
        }

    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(positionA.position, positionB.position);
    }

}
