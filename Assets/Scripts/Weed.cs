using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    PlayerControls controls;
    PlayerCar car;

    [SerializeField] GameObject joint;
    
    float jointSpeed = 0.00000002728f;
    bool jointOnScreen = false;

    [SerializeField] GameObject clipper;
    Vector3 clipperFinalPos = new Vector3(-0.002575003f, 0.005049f, -0.0005985805f);
    Vector3 clipperSpeed;
    [SerializeField] ParticleSystem fire;
    bool immidateStop = true;
    Vector3 fireScale;
    [SerializeField] Light clipperLight;

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
        fireScale = fire.transform.localScale;
        fire.transform.localScale = Vector3.zero;
        clipperSpeed = -(clipperFinalPos - clipper.transform.localPosition) / 200f;
        clipperLight.enabled = false;
    }

    void StopJoint()
    {
        CancelInvoke("MoveJointAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
            clipper.SetActive(false);
        }
           

        if (jointOnScreen)
        {
            if(immidateStop)
                fire.transform.localScale = fireScale;
            else fire.Play();
            clipperLight.enabled = true;
        }
            

        counter = 0;
    }

    

    public void ToggleJoint()
    {
        if (!IsInvoking("StopJoint"))
        {
            jointOnScreen = !jointOnScreen;
            joint.SetActive(true);
            clipper.SetActive(true);
            car.ResetCamera();
            clipperSpeed = -clipperSpeed;
            jointSpeed = -jointSpeed;
            InvokeRepeating("MoveJointAnimation", 0f, 0.01f);
            Invoke("StopJoint", 2f);
        }
    }

    int counter = 0;
    void MoveJointAnimation()
    {
        if (counter == 0 && !jointOnScreen)
        {
            if(immidateStop)
                fire.transform.localScale = Vector3.zero;
            else fire.Stop();
            clipperLight.enabled = false;
        }


        clipper.transform.localPosition += clipperSpeed;
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
        counter++;
    }

}
