using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMode : MonoBehaviour
{
    RaycastHit hit;
    Transform originalTrans;
    PlayerCar car;
    Camera myCamera;
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    float timeToChange = 30f;
    float littleNum = /*1.75f*/ 0.1f;
    public static bool active = false;

    void Start()
    {
        car = FindObjectOfType<PlayerCar>();
        myCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (active)
        {
            myCamera.transform.LookAt(car.transform);
        }

        float maxDistance = Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)) - littleNum;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
        if (!IsInvoking("Check") && hits.Length > 0/*Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)*/)
        {
            StartCheck();
        }
    }

    float counter = 1;
    int timesCounter = 0;
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
        float maxDistance = Mathf.Abs(Vector3.Distance(transform.position, car.transform.position)) - littleNum;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
        if (/*Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)*/hits.Length > 1)
        {
            if (counter <= 0)
            {
                CancelInvoke("Check");
                CancelInvoke("ChangePosition");
                StartCinematic();
                counter = 1f;
                print("restarting!" + ++timesCounter);
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
        myCamera.transform.localPosition = positions[0]; //still not working right, camera goes to wrong direction if car is headed wrong!
        car.ResetCamera();
        //myCamera.transform.rotation = originalTrans.rotation;
    }
    
    void ChangePosition()
    {
        myCamera.transform.localPosition = positions[Random.Range(1, positions.Count)];
    }
}
