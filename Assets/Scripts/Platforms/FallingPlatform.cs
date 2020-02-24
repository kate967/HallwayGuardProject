using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject player;
    public float fallingTime = 0.3f;
    public bool rotated90;

    private Rigidbody rb;
    private Vector3 origPos;
    private bool hasFallen;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        origPos = transform.position;
    }

    void Update()
    {
        if(hasFallen)
        {
            StartCoroutine(WaitAfterFallen(4));
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StartCoroutine(Wait(fallingTime));
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return (new WaitForSeconds(time));
        rb.isKinematic = false;
        hasFallen = true;
    }

    private IEnumerator WaitAfterFallen(float time)
    {
        yield return (new WaitForSeconds(time));
        transform.position = origPos;
        if(rotated90)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        if(!rotated90)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        rb.isKinematic = true;
        hasFallen = false;
    }
}
