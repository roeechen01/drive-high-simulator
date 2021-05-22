using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMode : MonoBehaviour
{
    PlayerControls controls;
    RaycastHit hit;
    Transform originalTrans;
    PlayerCar car;
    Camera myCamera;
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    float timeToChange = 30f;
    public static bool active = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ResetCamera.performed += ctx => ResetCamera();
    }

    void Start()
    {
        car = FindObjectOfType<PlayerCar>();
        myCamera = GetComponentInChildren<Camera>();
    }

    void ResetCamera()
    {
        if (CinematicMode.active)
        {
            myCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            myCamera.transform.LookAt(car.transform);
        }
           
    }

    bool NonCarHit(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.GetComponent<PlayerCar>())
                return true;
        }
        return false;
    }

    void Update()
    {
        //if (active)
            //myCamera.transform.LookAt(car.transform);

        if (!IsInvoking("Check") && NonCarHit(Physics.RaycastAll(transform.position, car.transform.position - myCamera.transform.position, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
            StartCheck();
        ChangeCameraDirection();
    }

    float counter = 1;
    void StartCheck()
    {
        counter = 1f;
        InvokeRepeating("Check", 0.1f, 0.1f);
    }

    public void SetOriginalTransform(Transform transform)
    {
        originalTrans = transform;
    }

    void Check()
    {
        counter -= 0.1f;
        if (NonCarHit(Physics.RaycastAll(transform.position, car.transform.position - myCamera.transform.position, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
        {
            if (counter <= 0)
            {
                CancelInvoke("Check");
                CancelInvoke("ChangePosition");
                StartCinematic();
                counter = 1f;
            }
        }
        else
        {
            CancelInvoke("Check");
            counter = 1f;
        }
    }

    public void Toggle()
    {
        if (active)
            EndCinematic();
        else StartCinematic();
    }

    void StartCinematic()
    {
        active = true;
        InvokeRepeating("ChangePosition", 0f, timeToChange);
    }

    void EndCinematic()
    {
        CancelInvoke("ChangePosition");
        active = false;
        myCamera.transform.localPosition = positions[0];
        car.ResetCamera();
        //myCamera.transform.rotation = originalTrans.rotation;
    }
    
    void ChangePosition()
    {
        myCamera.transform.localPosition = positions[Random.Range(1, positions.Count)];
        myCamera.transform.LookAt(car.transform);
        if (NonCarHit(Physics.RaycastAll(transform.position, transform.forward, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
            ChangePosition();
    }

    void ChangeCameraDirection()
    {
        if (CinematicMode.active)
        {
            Vector2 view = car.GetView();
            float rotationSpeed = 50f;
            myCamera.transform.Rotate(-view.y * Time.deltaTime * rotationSpeed, view.x * Time.deltaTime * rotationSpeed, 0f);
            myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y, 0f);
            //myCamera.transform.localRotation = Quaternion.Euler(myCamera.transform.localRotation.eulerAngles.x, myCamera.transform.localRotation.eulerAngles.y, 0f);
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

}

