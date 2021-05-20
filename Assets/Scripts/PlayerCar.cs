using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCar : MonoBehaviour
{
    Camera myCamera;
    CameraShake cameraShake;
    public AudioSource carSounds;
    [SerializeField] AudioClip[] lightCrashSounds;
    [SerializeField]  AudioClip[] heavyCrashSounds;
    [SerializeField] AudioClip[] breaksSounds;
    [SerializeField] AudioClip carCrash;
    Rigidbody myRigidbody;
    float tick = 0.1f;
    float soundAddition = 2f;
    float speedAddition = 250f;
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
    public float buildDifference = 5.5f;
    Radio radio;
    public AudioSource engine;
    bool r3 = false;
    Quaternion cameraDefaultRotation;
    bool onRoad = true;
    bool onHardCollision = false;
    float notOnRoadDiff = 0.75f;
    Vector3 lastVelocity;
    Vector3 lastPosition;
    Quaternion lastRotation;

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

    float gasMax;
    float brakesForSound = 0.75f;
    bool finishedBrakes = true;
    void PlayBrakes() {
        if(reverseAmount >= brakesForSound && !IsInvoking("SetCrash") && finishedBrakes)
        {
            carSounds.Stop();
            carSounds.clip = breaksSounds[UnityEngine.Random.Range(0, breaksSounds.Length)];
            if (reverseAmount * (currentSpeed / gasMax) > 1)
                carSounds.volume = 1;
            else
                carSounds.volume = reverseAmount * (currentSpeed / gasMax);

            carSounds.Play();
            finishedBrakes = false;
        }
    }

    void CheckPedals()
    {
        gasMax = 18750f / buildDifference;
        float reverseMax = -gasMax / 2, noGasDiv = 15f, minSound = 10f;
       
        if (/*!*/(reverseAmount > 0.2f) || /*!*/(gasAmount > 0.2f) || !(currentSpeed == 0))
        {
            if (reverseAmount < brakesForSound)
                finishedBrakes = true;

            if (reverseAmount > 0.2f && currentSpeed > reverseMax)
            {
                if (currentSpeed > 0)
                {
                    if(reverseAmount >= brakesForSound && currentSpeed > 0.225f * gasMax && !IsInvoking("BreaksCooldown"))
                    {
                        Invoke("PlayBrakes", 0.25f);
                    }
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
        cameraShake = myCamera.GetComponent<CameraShake>();
        radio = GetComponent<Radio>();
        cameraDefaultRotation = Quaternion.Euler(10, 0, 0);
        ResetCamera();
        InvokeRepeating("Timer", 0f, tick);
        InvokeRepeating("CheckFlying", 0.5f, 0.5f);

        myCamera.transform.position = new Vector3(transform.position.x - 0.35f, transform.position.y + 0.95f, transform.position.z - 0.2f);
        InvokeRepeating("CheckPedals", 0.1f, 0.1f);

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

    void CheckFlying()
    {
        if (Mathf.Abs(myRigidbody.velocity.y) > 0.2f)
        {
            transform.position = lastPosition;
            myRigidbody.velocity = Vector3.zero;
            currentSpeed = 0f;
            transform.rotation = lastRotation;
            //print("fixing!");
        }
        else UpdatePositions();
    }

    void UpdatePositions()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        lastVelocity = myRigidbody.velocity;
    }

    void Timer()
    {
        TimeNow += tick;
        radio.CheckForNextFreestyleBeat();
    }
    void SetCrash()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("MassObjects"))
        {
            float multiplier = 1f;
            float soundMultiplier = 1f;
            if (collisionObject.GetComponent<Rigidbody>())
            {
                float insigignificant = gasMax / 3.5f;
                float mass = collisionObject.GetComponent<Rigidbody>().mass;
                soundMultiplier = mass;
                if (mass == 1)
                {
                    onHardCollision = true;
                    if (collisionObject.gameObject.GetComponent<ScenaryCar>() && currentSpeed > insigignificant)
                    {
                        carSounds.clip = carCrash;
                    }
                    else
                    {
                        if (currentSpeed < insigignificant)
                        {
                            cameraShake.Shake((currentSpeed / gasMax), 0.15f);
                            carSounds.clip = lightCrashSounds[UnityEngine.Random.Range(0, lightCrashSounds.Length)];
                        }
                            
                        else {
                            cameraShake.Shake((currentSpeed / gasMax), 0.2f);
                            carSounds.clip = heavyCrashSounds[UnityEngine.Random.Range(0, heavyCrashSounds.Length)];
                        }
                    }
                }

                else
                {
                    carSounds.clip = lightCrashSounds[UnityEngine.Random.Range(0, lightCrashSounds.Length)];
                    cameraShake.Shake((currentSpeed / gasMax) * mass, 0.15f);
                }

                
                if (!IsInvoking("SetCrash")) {
                    soundMultiplier *= Mathf.Abs(currentSpeed) / 15000f;
                    carSounds.volume = soundMultiplier;
                    carSounds.Play(); 
                }

                multiplier = this.myRigidbody.mass - collisionObject.GetComponent<Rigidbody>().mass;
                currentSpeed *= multiplier;
            }
        }  
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.CompareTag("MassObjects"))
        {
            Invoke("SetCrash", 1f);
        }

        if (collisionObject.tag == "Road")
            onRoad = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collision.gameObject.tag == "MassObjects" && collisionObject.GetComponent<Rigidbody>() && collisionObject.GetComponent<Rigidbody>().mass == 1)
            onHardCollision = false;
        if (collisionObject.tag == "Road")
            onRoad = false;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collisionObject = other.gameObject;
        if (collisionObject.CompareTag("Flower"))
        {
            if(collisionObject.name.Substring(0, 5) == "Daisy")
                collisionObject.transform.localScale = new Vector3(collisionObject.transform.localScale.x, collisionObject.transform.localScale.y / 5, collisionObject.transform.localScale.z);
            else collisionObject.transform.localScale = new Vector3(collisionObject.transform.localScale.x * 2f, collisionObject.transform.localScale.y / 5, collisionObject.transform.localScale.z * 2f);
        }
    }
}
