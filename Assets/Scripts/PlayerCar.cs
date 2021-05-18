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
    float cameraSpeedRightLeft = 180f;
    float cameraSpeedUpDown = 120f;
    PlayerControls controls;
    Vector2 direction;
    Vector2 view;
    float gasAmount;
    float reverseAmount;
    //public bool build = false;
    public float buildDifference = 5.5f;
    Radio radio;
    public AudioSource engine;
    bool r3 = false;
    Quaternion cameraDefaultRotation;
    bool onRoad = true;
    bool onHardCollision = false;
    float notOnRoadDiff = 0.75f;

    public float TimeNow { get; set; } = 0f;


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
        engine.volume = 0.3f/*0.8f*/ * (currentSound / 100);

       // if (this.myRigidbody.velocity == Vector3.zero)
           // currentSpeed = 0;

        if(currentSpeed >= 0)
        {
            float maxSpeed = 120;
            float speedInKph = (Mathf.Abs(currentSpeed) / gasMax) * maxSpeed;
            if(onHardCollision)
                radio.speedText.text = "KPH: " + 0;
            else if(onRoad)
                radio.speedText.text = "KPH: " + Math.Round(speedInKph).ToString();
            else radio.speedText.text = "KPH: " + Math.Round((notOnRoadDiff * speedInKph)).ToString();
        }
        else radio.speedText.text = "R";

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
        if(onRoad)
            myRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;
        else myRigidbody.velocity = transform.forward * (currentSpeed * notOnRoadDiff) * Time.deltaTime;
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<Camera>();
        radio = GetComponent<Radio>();
        cameraDefaultRotation = Quaternion.Euler(10, 0, 0);
        ResetCamera();
        InvokeRepeating("Timer", 0f, tick);

        myCamera.transform.position = new Vector3(transform.position.x - 0.35f, transform.position.y + 0.95f, transform.position.z - 0.2f);
        InvokeRepeating("CheckPedals", 0.1f, 0.1f);

        /*if (build)
            speedAddition /= buildDifference;
        else buildDifference = 1;*/
        if (!Application.isEditor)
            speedAddition /= buildDifference;
        else buildDifference = 1;
    }

    void ChangeCameraDirection()
    {
        float stickError = 0.1f, back = 0.075f;
        if (view.x > stickError || -view.x > stickError || view.y > stickError || -view.y > stickError)
        {
            if (!r3)
            {
                if (myCamera.transform.localRotation.eulerAngles.y < 75 || myCamera.transform.localRotation.eulerAngles.y > 285)
                {
                    myCamera.transform.Rotate(new Vector3(0, view.x, 0) * cameraSpeedRightLeft * Time.deltaTime);
                }
                else
                {
                    if (myCamera.transform.localRotation.eulerAngles.y <= 275)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y - back, 0f);

                    if (myCamera.transform.localRotation.eulerAngles.y >= 85)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y + back, 0f);
                }
                if (Mathf.Abs(view.y) > 0.2f && myCamera.transform.localRotation.eulerAngles.x < 75 || myCamera.transform.localRotation.eulerAngles.x > 285)
                {
                    myCamera.transform.Rotate(new Vector3(-view.y, 0, 0) * cameraSpeedUpDown * Time.deltaTime);
                }
                else
                {
                    if (myCamera.transform.localRotation.eulerAngles.x <= 275)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x - back, myCamera.transform.localRotation.eulerAngles.y, 0f);

                    if (myCamera.transform.localRotation.eulerAngles.x >= 85)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x + back, myCamera.transform.localRotation.eulerAngles.y, 0f);
                }
            }
            else
            {
                if (myCamera.transform.localRotation.eulerAngles.y < 255 && myCamera.transform.localRotation.eulerAngles.y > 105)
                {
                    myCamera.transform.Rotate(new Vector3(0, view.x, 0) * cameraSpeedRightLeft * Time.deltaTime);
                }
                else
                {
                    if (myCamera.transform.localRotation.eulerAngles.y >= 115)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y - back, 0f);

                    if (myCamera.transform.localRotation.eulerAngles.y <= 245)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y + back, 0f);
                }

                if (myCamera.transform.localRotation.eulerAngles.x < 75 || myCamera.transform.localRotation.eulerAngles.x > 295)
                {
                    myCamera.transform.Rotate(new Vector3(-view.y, 0, 0) * cameraSpeedUpDown * Time.deltaTime);
                }
                else
                {
                    if (myCamera.transform.localRotation.eulerAngles.x >= 285)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x + back, myCamera.transform.localRotation.eulerAngles.y, 0f);

                    if (myCamera.transform.localRotation.eulerAngles.x <= 85)
                        myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x - back, myCamera.transform.localRotation.eulerAngles.y, 0f);
                }
            }
            myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y, 0f);
        }
          
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
        if(!r3)
            myCamera.transform.localRotation = cameraDefaultRotation;
    }

    void ReverseCamera()
    {
        float heightDiff = 0.002f;
        myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
        r3 = !r3;
        if (r3)
            myCamera.transform.localPosition = new Vector3(myCamera.transform.localPosition.x, myCamera.transform.localPosition.y + heightDiff, myCamera.transform.localPosition.z);
        else myCamera.transform.localPosition = new Vector3(myCamera.transform.localPosition.x, myCamera.transform.localPosition.y - heightDiff, myCamera.transform.localPosition.z);
    }

    void Timer()
    {
        TimeNow += tick;
        radio.CheckForNextFreestyleBeat();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hard")
        {
            onHardCollision = true;
            currentSpeed = 0;
        }
        if (collision.gameObject.tag == "Soft")
        {
            float multiplier = 1f;
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                multiplier = this.myRigidbody.mass - collision.gameObject.GetComponent<Rigidbody>().mass;
                currentSpeed *= multiplier;
                print("entered");
            }
                
        }
            

       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Hard")
            onHardCollision = false;
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Road")
            onRoad = true;
        print("enter");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Road")
            onRoad = false;
        print("exit");
    }*/
}
