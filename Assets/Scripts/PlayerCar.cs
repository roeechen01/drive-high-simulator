using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCar : MonoBehaviour
{
    Camera myCamera;
    Rigidbody myRigidbody;
    float tick = 0.1f;
    float soundAddition = 2f;
    float speedAddition = 240f;
    float currentSound = 0f;
    float currentSpeed = 0f;
    float rotationSpeed = 125f;
    PlayerControls controls;
    Vector2 direction;
    Vector2 view;
    float gasAmount;
    float reverseAmount;
    Vector2 last = Vector2.zero;
    public bool build = false;
    public float buildDifference = 5.5f;
    Radio radio;
    public AudioSource engine;
    float time = 0f;
    bool r3 = false;
    bool lastR3 = false;
    

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Gas.performed += ctx => gasAmount = ctx.ReadValue<float>();

        controls.Gameplay.Reverse.performed += ctx => reverseAmount = ctx.ReadValue<float>();

        controls.Gameplay.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => direction = Vector2.zero;

        controls.Gameplay.View.performed += ctx => view = ctx.ReadValue<Vector2>();
        controls.Gameplay.View.canceled += ctx => view = Vector2.zero;

        controls.Gameplay.ResetCamera.performed += ctx => ResetCamera();

        controls.Gameplay.ReverseCamera.performed += ctx => ReverseCamera();
        controls.Gameplay.ReverseCamera.canceled += ctx => ReverseCamera();

        controls.Gameplay.Quit.performed += ctx => Application.Quit();

        

        controls.Gameplay.Restart.performed += ctx => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<Camera>();
        radio = GetComponent<Radio>();
        InvokeRepeating("Timer", 0f, tick);
       
        myCamera.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.8f, transform.position.z - 0.6f);
        InvokeRepeating("CheckPedals", 0.1f, 0.1f);
        
        //InvokeRepeating("ChangeCameraDirection", 0f, 0.01f);

        if (build)
            speedAddition /= buildDifference;
        else buildDifference = 1;
    }

    void CheckPedals()
    {
        float reverseMax = -7500f / buildDifference, gasMax = 15000f / buildDifference, noGasDiv = 15f, minSound = 10f;

        if (!(reverseAmount > 0.2f) || !(gasAmount > 0.2f) || !(currentSpeed == 0))
        {
            if (reverseAmount > 0.2f && currentSpeed > reverseMax)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= speedAddition * reverseAmount * 2;
                    currentSound -= soundAddition * 2;
                }

                else
                {
                    currentSpeed -= speedAddition * reverseAmount;
                    currentSound += soundAddition;
                }
            }
            else if (gasAmount > 0.2f && currentSpeed < gasMax)
            {
                currentSpeed += speedAddition * gasAmount;
                currentSound += soundAddition / 2;
            }

            else if (currentSpeed > 0)
            {
                currentSpeed -= speedAddition / 5;
                currentSound -= soundAddition / noGasDiv;
                if (currentSpeed <= speedAddition) currentSpeed = 0;
                if (currentSound <= 0) currentSound = 0;
            }

            else
            {
                if (currentSpeed < 0)
                {
                    currentSpeed += speedAddition / 5;
                    currentSound -= soundAddition / noGasDiv;
                    if (currentSpeed >= speedAddition) currentSpeed = 0;
                    if (currentSound <= 0) currentSound = 0;
                }
                else if (currentSpeed == 0)
                    currentSound -= soundAddition;
            }
        }

        if (currentSound < minSound)
            currentSound = minSound;
        else if (currentSound > 100)
            currentSound = 100;
        //if (currentSound <= soundAddition && Mathf.Abs(currentSpeed) > 0)
        // currentSound = (Mathf.Abs(currentSpeed) / -1 * reverseMax) * 100;
        engine.volume = 0.8f * (currentSound / 100);
    }

    void Update()
    {
        ChangeSpeed();
        ChangeCameraDirection();
    }

    private void ChangeSpeed()
    {
        if (currentSpeed != 0)
            transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
        myRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;
    }

    void ChangeCameraDirection()
    {
        float stickError = 0.1f, humanError = 0.05f;
        if ((lastR3 != r3) || view.x > stickError || -view.x > stickError || view.y > stickError || -view.y > stickError)
        {
            //myCamera.transform.Rotate(new Vector3(0, view.x, 0) * cameraSpeed * Time.deltaTime);//for one direction viewing
            //myCamera.transform.Rotate(new Vector3(-view.y, view.x, 0) * cameraSpeed * Time.deltaTime);//for two directions viewing
            if (Vector2.Distance(last, view) > humanError)
            {
                float multiplier = 75f;

                if (lastR3 == r3)
                {
                    myCamera.transform.localRotation = Quaternion.Euler(-view.y * multiplier, view.x * multiplier, 0f);
                    if (r3)
                    {
                        myCamera.transform.localRotation = Quaternion.Euler(view.y * multiplier, view.x * multiplier, 0f);
                        myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
                    }
                }
                else
                {
                    myCamera.transform.localRotation = Quaternion.Euler(-view.y * multiplier, view.x * multiplier, 0f);
                    myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
                }
                last = view;
            }
            else
            {
                if (lastR3 != r3)
                    myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
            }
        }
        else
        {
            myCamera.transform.localRotation = Quaternion.identity;
            if (r3)
                myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
        }
        lastR3 = r3;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void ResetCamera()
    {
        myCamera.transform.localRotation = Quaternion.identity;
    }

    void ReverseCamera()
    {
        //myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
        r3 = !r3;
    }

    public float TimeNow
    {
        get { return time; }
        set {time = value; }
    }

    void Timer()
    {
        time += tick;
        radio.CheckForNextFreestyleBeat();
    }
}
