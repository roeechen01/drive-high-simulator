using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloon : MonoBehaviour
{
    float speed = 2f;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0f, speed, 0f);
        InvokeRepeating("ChangeDirection", 0f, 10f);
    }

    void ChangeDirection()
    {
        myRigidbody.velocity = Vector3.zero;
        Invoke("SetVelocity", 1f);
    }

    void SetVelocity()
    {
        speed = -speed;
        myRigidbody.velocity = new Vector3(0f, speed, 0f);
    }

}
