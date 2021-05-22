using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMode : MonoBehaviour
{
    Transform originalTrans;
    PlayerCar car;
    Camera myCamera;
    public static bool active = false;
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    float timeToChange = 15f;

    void Start()
    {
        car = FindObjectOfType<PlayerCar>();
        myCamera = GetComponentInChildren<Camera>();
        
    }

    public void SetOriginalTransform (Transform transform)
    {
        originalTrans = transform;
    }

    void Update()
    {
        if (active)
        {
            myCamera.transform.LookAt(car.transform);
            if (!car.myRenderer.isVisible)
            {
                CancelInvoke("ChangePosition");
                InvokeRepeating("ChangePosition", 0f, timeToChange);
            }
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
        myCamera.transform.localPosition = positions[0]; //still not working right, camera goes to wrong direction if car is headed wrong!
        car.ResetCamera();
        //myCamera.transform.rotation = originalTrans.rotation;
    }
    
    void ChangePosition()
    {
        myCamera.transform.localPosition = positions[Random.Range(1, positions.Count)];
    }
}
