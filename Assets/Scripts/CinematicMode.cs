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
        if(active)
            myCamera.transform.LookAt(car.transform);
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
        InvokeRepeating("ChangePosition", 0f, 10f);
    }

    void EndCinematic()
    {
        CancelInvoke("ChangePosition");
        active = false;
        myCamera.transform.position = car.transform.position + positions[0];
        car.ResetCamera();
        //myCamera.transform.rotation = originalTrans.rotation;
    }
    
    void ChangePosition()
    {
        myCamera.transform.position = car.transform.position + positions[Random.Range(1, positions.Count)];
    }
}
