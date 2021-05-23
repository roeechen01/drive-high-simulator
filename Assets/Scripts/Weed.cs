using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    PlayerControls controls;
    PlayerCar car;

    [SerializeField] GameObject joint;
    float jointSpeed = 0.00000002728f;
    bool on = false;

    [SerializeField] GameObject clipper;
    Camera face;
    [SerializeField] bool NSFW;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ToggleJoint.performed += ctx => ToggleJoint();
    }


    void Start()
    {
        face = GetComponent<Camera>();
        car = FindObjectOfType<PlayerCar>();        
    }

    void StopJoint()
    {
        CancelInvoke("MoveJointAnimation");
        if(!on)
            joint.SetActive(false);
    }

    public void ToggleJoint()
    {
        if (!IsInvoking("StopJoint"))
        {
            on = !on;
            joint.SetActive(true);
            car.ResetCamera();
            jointSpeed = -jointSpeed;
            InvokeRepeating("MoveJointAnimation", 0f, 0.01f);
            Invoke("StopJoint", 2f);
        }
    }

    void MoveJointAnimation()
    {
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
    }

}
