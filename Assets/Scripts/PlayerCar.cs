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
    float rotationSpeed = 150f;
    float cameraSpeed = 400f;
    PlayerControls controls;
    Vector2 direction;
    Vector2 view;
    float gasAmount;
    float reverseAmount;
    bool gas = true;
    bool reverse = true;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Gas.performed += ctx => gasAmount = ctx.ReadValue<float>();
        //controls.Gameplay.Gas.performed += ctx => Gas();
        controls.Gameplay.Gas.canceled += ctx => Stop();

        controls.Gameplay.Reverse.performed += ctx => reverseAmount = ctx.ReadValue<float>();
        controls.Gameplay.Reverse.canceled += ctx => Stop();

        controls.Gameplay.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => direction = Vector2.zero;

        controls.Gameplay.View.performed += ctx => view = ctx.ReadValue<Vector2>();
        controls.Gameplay.View.canceled += ctx => view = Vector2.zero;

        controls.Gameplay.ResetCamera.performed += ctx => ResetCamera();

        controls.Gameplay.ReverseCamera.performed += ctx => ReverseCamera();
        controls.Gameplay.ReverseCamera.canceled += ctx => ReverseCamera();

        controls.Gameplay.Quit.performed += ctx => Application.Quit();


    }

    // Start is called before the first frame update
    void Start()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
            //Application.Quit();
        myRigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<Camera>();
        myCamera.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.8f, transform.position.z - 0.6f);
        //myCamera.transform.rotation = transform.rotation;
        InvokeRepeating("CheckGas", 0.1f, 0.5f);
    }

    // Update is called once per frame

    void CheckGas()
    {
        float reverseMax = -7500, gasMax = 15000;
        if (gasAmount > 0.2f && currentSpeed < gasMax)
            currentSpeed += speedAddition * gasAmount;
        else if (reverseAmount > 0.2f && currentSpeed > reverseMax)
            currentSpeed -= speedAddition * reverseAmount * 2;
        else if (currentSpeed > 0)
        {
            currentSpeed -= speedAddition / 5;
            if (currentSpeed <= speedAddition) currentSpeed = 0;
        }

        else
        {
            if (currentSpeed < 0) currentSpeed += speedAddition / 5;
            if (currentSpeed >= speedAddition) currentSpeed = 0;
        }
        
    }

    void Update()
    {
        gas = gasAmount > 0.2f;
        reverse = reverseAmount > 0.2f;

        if (currentSpeed != 0)
            transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
        myRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;

        /*if (myRigidbody.velocity != Vector3.zero)
        {
            
            if (gas)
            {
                transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
                myRigidbody.velocity = transform.forward * (gasAmount) * speedAddition * Time.deltaTime;
            }
            else
            {
               
                if (!reverse)
                {
                    //myRigidbody.velocity *= 0.5f;
                    //myRigidbody.velocity = transform.forward * Time.deltaTime * speed;
                    //myRigidbody.velocity += -transform.forward;
                    //if (myRigidbody.velocity.magnitude < 1)
                    
                    myRigidbody.velocity = Vector3.zero;
                }
                else
                {
                    transform.Rotate(Vector3.up * -direction.x * rotationSpeed * Time.deltaTime);
                    myRigidbody.velocity = transform.forward * (reverseAmount) * -speedAddition * Time.deltaTime;
                }

            }
        }
        else
        {
            if (gas)
            {
                //transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
                myRigidbody.velocity += transform.forward * (gasAmount) * speedAddition * Time.deltaTime;
            }
            if (reverse)
            {
                myRigidbody.velocity += transform.forward * (reverseAmount) * -speedAddition * Time.deltaTime;
            }
        }/*

            /*if (gasAmount > 0)
        {
            transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
            myRigidbody.velocity += transform.forward * (gasAmount * 1f) * speed * Time.deltaTime;

        }
        else if (breaksAmount == 0)
        {
            //print("Entered: " + myRigidbody.velocity);
            if (myRigidbody.velocity != Vector3.zero)
            {
                transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
                //myRigidbody.velocity -= transform.forward * 1000f * Time.deltaTime;
                myRigidbody.velocity /= 2f * Time.deltaTime;
                //print("Entered: " + myRigidbody.velocity);
            }
        }*/

        if (myRigidbody.velocity != Vector3.zero)
        {
            //if (gas)
            //{
                //transform.Rotate(Vector3.up * direction.x * rotationSpeed * Time.deltaTime);
                //myRigidbody.velocity = transform.forward * Time.deltaTime; 
            //}
            //else
            //{
                //transform.Rotate(Vector3.up * -direction.x * rotationSpeed * Time.deltaTime);
                //myRigidbody.velocity = transform.forward * -speed * Time.deltaTime;
            //}
        }
        
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

    void Gas()
    {
        //gas = true;
        myRigidbody.velocity = transform.forward * speedAddition * Time.deltaTime;
        
    }

    void ResetCamera()
    {
        myCamera.transform.rotation = transform.rotation;
    }

    void ReverseCamera()
    {
        myCamera.transform.Rotate(new Vector3(0, 1, 0), 180f);
        print("called");
    }

    /*void Repeat()
    {
        if (myRigidbody.velocity != Vector3.zero)
        {
            if (gas)
            {
                transform.Rotate(Vector3.up * direction2.x * rotationSpeed * Time.deltaTime);
                myRigidbody.velocity = transform.forward * speed * Time.deltaTime;
            }
            else
            {
                transform.Rotate(Vector3.up * -direction2.x * rotationSpeed * Time.deltaTime);
                myRigidbody.velocity = transform.forward * -speed * Time.deltaTime;
            }
        }
        else CancelInvoke("Repeat");
        print(direction2);
            
    }*/

    void Reverse()
    {
        //gas = false;
        myRigidbody.velocity = transform.forward * -speedAddition * Time.deltaTime;
    }

    void Stop()
    {
        myRigidbody.velocity = Vector3.zero;
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
    }*/

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
        

    }
}
