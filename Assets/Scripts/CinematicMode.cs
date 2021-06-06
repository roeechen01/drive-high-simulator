using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMode : MonoBehaviour
{
    PlayerControls controls;
    RaycastHit hit;
    Transform originalTrans;
    PlayerCar car;
    Camera face;
    Camera cinematicCamera;

    public List<Vector3> positions = new List<Vector3>();
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
        face = FindFaceCamera();
        cinematicCamera = GetComponent<Camera>();
    }

    public static Camera FindFaceCamera()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach(Camera camera in cameras)
        {
            if (!camera.GetComponent<CinematicMode>())
                return camera;
        }
        return null;
    }

    void ResetCamera()
    {
        if (CinematicMode.active)
        {
            face.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            face.transform.LookAt(car.transform);
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

        if (!IsInvoking("Check") && NonCarHit(Physics.RaycastAll(transform.position, car.transform.position - cinematicCamera.transform.position, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
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
        if (NonCarHit(Physics.RaycastAll(transform.position, car.transform.position - cinematicCamera.transform.position, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
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
        face.enabled = false;
        face.GetComponent<AudioListener>().enabled = false;
        cinematicCamera.enabled = true;
        cinematicCamera.GetComponent<AudioListener>().enabled = true;
        active = true;
        InvokeRepeating("ChangePosition", 0f, timeToChange);
    }

    void EndCinematic()
    {
        face.enabled = true;
        face.GetComponent<AudioListener>().enabled = true;
        cinematicCamera.enabled = false;
        face.GetComponent<AudioListener>().enabled = false;
        CancelInvoke("ChangePosition");
        active = false;
        cinematicCamera.transform.localPosition = positions[0];
        car.ResetCamera();
        //myCamera.transform.rotation = originalTrans.rotation;
    }
    
    void ChangePosition()
    {
        cinematicCamera.transform.localPosition = positions[Random.Range(1, positions.Count)];
        cinematicCamera.transform.LookAt(car.transform);
        if (NonCarHit(Physics.RaycastAll(transform.position, transform.forward, Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)))))
            ChangePosition();
    }

    void ChangeCameraDirection()
    {
        if (CinematicMode.active)
        {
            Vector2 view = car.GetView();
            float rotationSpeed = 50f;
            cinematicCamera.transform.Rotate(-view.y * Time.deltaTime * rotationSpeed, view.x * Time.deltaTime * rotationSpeed, 0f);
            cinematicCamera.transform.localRotation = Quaternion.Euler(cinematicCamera.transform.localRotation.eulerAngles.x, cinematicCamera.transform.localRotation.eulerAngles.y, 0f);
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

