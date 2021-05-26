using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    PlayerControls controls;
    PlayerCar car;

    [SerializeField] GameObject joint;

    float jointSpeed = 0.00000002728f;
    bool jointOnScreen = false, clipperOnScreen = false;
    bool jointUsed = false, clipperUsed = false;
    [SerializeField] ParticleSystem lit;
    [SerializeField] Light litLight;

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

        if (jointLit)
        {
            lit.transform.localScale = new Vector3(0.008f, 0.01f, 0.008f);
            litLight.enabled = true;
        }
        else
        {
            lit.transform.localScale = Vector3.zero;
            litLight.enabled = false;
        }
    }


    float timeLit = 0f;
    bool jointLit = false;

    void IncreaseLitTime()
    {
        timeLit += 0.1f;
        if (timeLit >= 1.5f && !jointLit)
        {
            jointLit = true;
            clipperSpeed = -clipperSpeed;
            InvokeRepeating("MoveClipperAnimation", 0f, 0.01f);
            Invoke("StopClipper", 2f);
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
        if (lastWorked)
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
        if(!clipperUsed && jointUsed && NoMoveAnimation())
        {
            clipperSpeed = -clipperSpeed;
            InvokeRepeating("MoveClipperAnimation", 0f, 0.01f);
            Invoke("StopClipper", 2f);
        }
        else if(jointOnScreen && clipper.activeSelf && clipperUsed && NoMoveAnimation())
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

    public bool SparkingUp()
    {
        return clipperUsed || IsInvoking("StopClipper") || IsInvoking("StopJointAndClipper");
    }

    public void StopLightClipper()
    {
        //if (jointOnScreen && clipper.activeSelf)
        //{
        if (IsInvoking("IncreaseLitTime") || valid)
        {
            //if (!IsInvoking("PushButton"))
            //{
                if (lastWorked)
                {
                    CancelInvoke("IncreaseLitTime");
                    clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z + 0.002f);
                }
                else
                {
                    clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z + 0.0015f);
                }
            //}
            

        }

        timeLit = 0;
        fire.transform.localScale = Vector3.zero;
        //}

    }

    void StopJointAndClipper()
    {
        CancelInvoke("MoveJointAndClipperAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
        }
        clipperUsed = !clipperUsed;
        jointUsed = !jointUsed;
    }

    void StopJoint()
    {
        CancelInvoke("MoveJointAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
        }
        jointUsed = !jointUsed;
    }

    void StopClipper()
    {
        CancelInvoke("MoveClipperAnimation");
        if (!clipperOnScreen)
        {
        }
        clipperUsed = !clipperUsed;
    }

    bool NoMoveAnimation()
    {
        return !IsInvoking("StopJointAndClipper") && !IsInvoking("StopJoint") && !IsInvoking("StopClipper");
    }

    public void ToggleJoint()
    {
        if (NoMoveAnimation())
        {
            if(jointUsed && !clipperUsed)
            {
                jointOnScreen = !jointOnScreen;
                joint.SetActive(true);
                car.ResetCamera();
                jointSpeed = -jointSpeed;
                InvokeRepeating("MoveJointAnimation", 0f, 0.01f);
                Invoke("StopJoint", 2f);
            }
            else
            {
                jointOnScreen = !jointOnScreen;
                clipperOnScreen = !clipperOnScreen;
                joint.SetActive(true);
                car.ResetCamera();
                clipperSpeed = -clipperSpeed;
                jointSpeed = -jointSpeed;
                InvokeRepeating("MoveJointAndClipperAnimation", 0f, 0.01f);
                Invoke("StopJointAndClipper", 2f);
            }
            
        }
    }

    void MoveClipperAnimation()
    {
        clipper.transform.localPosition += clipperSpeed;
    }

    void MoveJointAnimation()
    {
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
    }

    void MoveJointAndClipperAnimation()
    {
        clipper.transform.localPosition += clipperSpeed;
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
    }

}