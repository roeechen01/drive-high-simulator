using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCar : MonoBehaviour
{
    Camera myCamera;
    Rigidbody myRigidbody;
    float speedAddition = 1000f;
    float currentSpeed = 0f;
    float rotationSpeed = 125f;
    float cameraSpeed = 400f;
    PlayerControls controls;
    Vector2 direction;
    Vector2 view;
    float gasAmount;
    float reverseAmount;
    public bool build = false;
    public float buildDifference = 6.5f;
    AudioSource audioSource;
    //AudioClip audioClip;

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

        controls.Gameplay.VolUp.performed += ctx => audioSource.volume += 0.1f;
        controls.Gameplay.VolDown.performed += ctx => audioSource.volume -= 0.1f;


    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        myRigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<Camera>();
        myCamera.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.8f, transform.position.z - 0.6f);
        InvokeRepeating("CheckPedals", 0.1f, 0.5f);
        if (build)
            speedAddition /= buildDifference;
        else buildDifference = 1;
    }

    void CheckPedals()
    {
        float reverseMax = -7500 / buildDifference, gasMax = 15000 / buildDifference;
        if (gasAmount > 0.2f && currentSpeed < gasMax)
            currentSpeed += speedAddition * gasAmount;
        else if (reverseAmount > 0.2f && currentSpeed > reverseMax)
        {
            if(currentSpeed > 0)
                currentSpeed -= speedAddition * reverseAmount * 2;
            else currentSpeed -= speedAddition * reverseAmount;
        }
            
        else if (currentSpeed > 0)
        {
            currentSpeed -= speedAddition / 5;
            if (currentSpeed <= speedAddition) currentSpeed = 0;
        }

        else
        {
            if (currentSpeed < 0)
            {
                currentSpeed += speedAddition / 5;
                if (currentSpeed >= speedAddition) currentSpeed = 0;
            }
        }
    }

    void Update()
    {
        if (currentSpeed != 0)
            transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
        myRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;
        
        if(view != Vector2.zero)
        {
            myCamera.transform.Rotate(new Vector3(0, view.x, 0) * cameraSpeed * Time.deltaTime);//for one direction viewing
            //myCamera.transform.Rotate(new Vector3(view.y, view.x, 0) * cameraSpeed * Time.deltaTime);//for two directions viewing
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
        myCamera.transform.rotation = transform.rotation;
    }

    void ReverseCamera()
    {
        myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
    }

    /*void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.velocity = transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            myRigidbody.velocity = Vector3.zero;
        }
       
        if (Input.GetKey(KeyCode.S))
        {
            myRigidbody.velocity = transform.forward * -speed * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }

    void ControlRotation()
    {
        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            transform.Rotate(Vector3.down * rotationSpeed);
        }
        
        if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            transform.Rotate(Vector3.up * rotationSpeed);
        }
    }*/
}
