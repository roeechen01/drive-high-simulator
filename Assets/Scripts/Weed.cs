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
    AudioSource clipperAudio;
    Vector3 clipperFinalPos = new Vector3(-0.002575003f, 0.005000f, -0.0005985805f);
    Vector3 clipperSpeed;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem sparks;
    Vector3 fireScale;
    [SerializeField] Light clipperLight;
    [SerializeField] Light sparksLight;
    [SerializeField] GameObject clipperButton;
    [SerializeField] GameObject clipperWheel;
    bool rotating = false;
    float rotationSpeed = 2000f;

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
        clipperAudio = clipper.GetComponent<AudioSource>();
        fireScale = fire.transform.localScale;
        fire.transform.localScale = Vector3.zero;
        clipperSpeed = -(clipperFinalPos - clipper.transform.localPosition) / 200f;
    }

    void Update()
    {
        if (rotating)
            clipperWheel.transform.Rotate(new Vector3(1f, 0f, 0f), Time.deltaTime * rotationSpeed);
        sparksLight.enabled = sparks.isPlaying;
        clipperLight.enabled = fire.transform.localScale != Vector3.zero;
    }


    float timeLit = 0f;
    bool jointLit = false;

    void IncreaseLitTime()
    {
        timeLit += 0.1f;
        if(timeLit >= 1.5f && !jointLit)
        {
            jointLit = true;
            print("Joint is lit");
        }
    }

    void RotateWheel(float rotateTime, float buttonDelay)
    {
        rotating = true;
        clipperAudio.Play();
        buttonDelay += Random.Range(-0.02f, 0.02f);//Added some random time to make it more realistic
        rotateTime += Random.Range(-0.03f, 0.03f);//Added some random time to make it more realistic
        Invoke("PushButton", buttonDelay);
        Invoke("SetRotatingFalse", rotateTime);
    }

    void PushButton()
    {
        if(lastWorked)
            clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.002f);
        else clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.0015f);
    }

    void SetRotatingFalse()
    {
        rotating = false;
    }

    bool valid = false, lastWorked = false;
    public void LightClipper()
    {
        if (jointOnScreen && clipper.activeSelf && Mathf.Abs(Vector3.Distance(clipper.transform.localPosition, clipperFinalPos)) < 0.0001f)
        {
            if (Random.Range(0, 2) == 1)
            {
                InvokeRepeating("IncreaseLitTime", 0.1f, 0.1f);
                fire.transform.localScale = fireScale;
                //clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.002f);
                RotateWheel(0.2f, 0.05f);
                lastWorked = true;
            }
            else
            {
                //clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.0015f);
                RotateWheel(0.15f, 0.1f);
                lastWorked = false;
                sparks.Play();
            }
            valid = true;
        }
        else valid = false;
    }


    public void StopLightClipper()
    {
        //if (jointOnScreen && clipper.activeSelf)
        //{
        if (IsInvoking("IncreaseLitTime") || valid)
        {
            if (lastWorked)
            {
                CancelInvoke("IncreaseLitTime");
                clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z + 0.002f);
            }
            else
            {
                clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z + 0.0015f);
            }
           
        }

        timeLit = 0;
        fire.transform.localScale = Vector3.zero;
        //}

    }

    void StopJoint()
    {
        CancelInvoke("MoveJointAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
            clipper.SetActive(false);
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
        clipper.transform.localPosition += clipperSpeed;
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
        counter++;
    }

}
