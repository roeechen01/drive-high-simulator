using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    PlayerControls controls;
    PlayerCar car;

    [SerializeField] GameObject joint;
    [SerializeField] GameObject scenary;

    AudioSource coughSource;
    [SerializeField] AudioClip[] coughSounds;

    List<ParticleSystem> jointSmokes = new List<ParticleSystem>();
    List<ParticleSystem> hitSmokes = new List<ParticleSystem>();


    float jointSpeed = 0.00000002728f;
    bool jointOnScreen = false, clipperOnScreen = false;
    bool jointUsed = false, clipperUsed = false, hitting = false;
    [SerializeField] ParticleSystem lit;
    [SerializeField] Light litLight;
    [SerializeField] ParticleSystem jointSmoke;
    [SerializeField] ParticleSystem hitSmoke;


    [SerializeField] GameObject clipper;
    AudioSource clipperAudio;
    Vector3 clipperFinalPos = new Vector3(-0.002575003f, 0.005000f, -0.0005985805f);
    Vector3 clipperSpeed;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem sparks;
    Vector3 fireScale;
    [SerializeField] Light clipperLight;
    [SerializeField] Light sparksLight;
    [SerializeField] GameObject clipperButton;
    Vector3 buttonStartPos;
    [SerializeField] GameObject clipperWheel;

    bool midHit = false;
    float hitTimer = 0;
    float high = 0;

    bool rotating = false;
    float rotationSpeed = 2000f;

    Camera face;
    [SerializeField] bool NSFW;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ToggleJoint.performed += ctx => ToggleJoint();
    }

    void Start()
    {
        face = GetComponent<Camera>();
        car = FindObjectOfType<PlayerCar>();
        coughSource = GetComponent<AudioSource>();
        clipperAudio = clipper.GetComponent<AudioSource>();
        fireScale = fire.transform.localScale;
        fire.transform.localScale = Vector3.zero;
        clipperSpeed = -(clipperFinalPos - clipper.transform.localPosition) / 200f;
        buttonStartPos = clipperButton.transform.localPosition;
        InvokeRepeating("SpawnJointSmoke", 1f, 0.5f);
        InvokeRepeating("DropHigh", 1f, 60f);
    }
    void Update()
    {
        if (rotating)
            clipperWheel.transform.Rotate(new Vector3(1f, 0f, 0f), Time.deltaTime * rotationSpeed);
        sparksLight.enabled = sparks.isPlaying;
        clipperLight.enabled = fire.transform.localScale != Vector3.zero;

        if (jointLit)
        {
            if (hitting)
            {
                lit.transform.localScale = new Vector3(0.01f, 0.012f, 0.01f);
                if (!midHit)
                {
                    midHit = true;
                    InvokeRepeating("AddToHitTimer", 0f, 0.1f);
                }
                    
            }

            else
            {
                lit.transform.localScale = new Vector3(0.007f, 0.009f, 0.007f);
            }

            litLight.enabled = true;
        }
        else
        {
            lit.transform.localScale = Vector3.zero;
            litLight.enabled = false;
        }

        if (IsInvoking("StopJointAndClipper") || IsInvoking("StopJoint"))
            jointLit = false;

        if (hitting && jointLit)
        {
            CancelInvoke("StopLit");
            Invoke("StopLit", 30f);
        }

    }

    void SpawnJointSmoke()
    {
        if(jointLit && jointSmokes.Count <= 50/*&& (jointSmokes.Count <= 0 || Mathf.Abs(Vector3.Distance(jointSmokes[jointSmokes.Count - 1].transform.position, lit.transform.position)) > 0.05f)*/)
        {
            jointSmokes.Add(Instantiate(jointSmoke, lit.transform.position, Quaternion.Euler(270f, 0f, 0f), car.transform));
            Invoke("DeleteJointSmoke", 10f);
            Invoke("SetJointSmokeScenary", 1f);
        }
          
    }

    void DeleteJointSmoke()
    {
        if(jointSmokes.Count > 0)
        {
            Destroy(jointSmokes[0]);
            jointSmokes.RemoveAt(0);
        }
    }

    void SetJointSmokeScenary()
    {
        for (int i = 0; i < jointSmokes.Count; i++)
        {

            if (!jointSmokes[i].transform.parent.name.Equals("Scenary"))
            {
                jointSmokes[i].transform.SetParent(scenary.transform, true);
                break;
            }
        }
    }

    public void Hit()
    {
        if(!coughSource.isPlaying)
            hitting = true;
    }

    public void StopHit()
    {
        if (hitting && jointLit)
        {
            Vector3 position = new Vector3(face.transform.localPosition.x - 0.4f, face.transform.localPosition.y + 0.75f, face.transform.localPosition.z - 1.75f);
            Quaternion rotation = Quaternion.Euler(face.transform.rotation.eulerAngles.x + 325f, face.transform.rotation.eulerAngles.y, face.transform.rotation.eulerAngles.z);
            ParticleSystem go = Instantiate(hitSmoke, position, rotation, car.transform);
            ParticleSystem.MainModule main = go.main;
            main.startSize = 5 + 4 * hitTimer;
            float alpha = 0.5f + hitTimer / 5f;
            main.startColor = new Color(1f, 1f, 1f, (alpha > 1) ? 1 : alpha);
            go.transform.localPosition = position;
            hitSmokes.Add(go);
            Invoke("DeleteHitSmoke", 5f);
            Invoke("SetHitSmokeScenary", 1f);
            hitting = false;
            midHit = false;
            if(Random.Range(0f, 5f) <= hitTimer)
            {
                coughSource.volume = 0.75f + hitTimer / 22; 
                coughSource.clip = coughSounds[Random.Range(0, coughSounds.Length)];
                coughSource.Play();
            }
            high += hitTimer;
            hitTimer = 0;
            CancelInvoke("AddToHitTimer");
            
        }
    }

    void DropHigh()
    {
        if(high >= 1)
            high -= 1f;
    }

    void AddToHitTimer()
    {
        hitTimer += 0.1f;
        if(hitTimer >= 5f)
            StopHit();
    }

    void DeleteHitSmoke()
    {
        if (hitSmokes.Count > 0)
        {
            Destroy(hitSmokes[0]);
            hitSmokes.RemoveAt(0);
        }
    }

    void SetHitSmokeScenary()
    {
        for(int i = 0; i < hitSmokes.Count; i++)
        {
            if (!hitSmokes[i].transform.parent.name.Equals("Scenary"))
            {
                hitSmokes[i].transform.SetParent(scenary.transform, true);
                break;
            }
        }
    }

    

    void StopLit()
    {
        jointLit = false;
    }


    float timeLit = 0f;
    bool jointLit = false;

    void IncreaseLitTime()
    {
        timeLit += 0.1f;
        if (timeLit >= 1.5f && !jointLit && hitting)
        {
            jointLit = true;
            clipperSpeed = -clipperSpeed;
            InvokeRepeating("MoveClipperAnimation", 0f, 0.01f);
            Invoke("StopClipper", 2f);
        }
    }

    void RotateWheel(float rotateTime, float buttonDelay)
    {
        rotating = true;
        clipperAudio.Play();
        buttonDelay += Random.Range(-0.02f, 0.02f);//Added some random time to make it more realistic
        rotateTime += Random.Range(-0.03f, 0.03f);//Added some random time to make it more realistic
        Invoke("PushButton", buttonDelay);
        Invoke("SetRotatingFalse", rotateTime);
    }

    void PushButton()
    {
        if (lastWorked)
            clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.002f);
        else clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.0015f);
    }

    void SetRotatingFalse()
    {
        rotating = false;
    }

    bool valid = false, lastWorked = false;
    public void LightClipper()
    {
        if(!clipperUsed && jointUsed && NoMoveAnimation() && !jointLit)
        {
            car.ResetCamera();
            clipperSpeed = -clipperSpeed;
            InvokeRepeating("MoveClipperAnimation", 0f, 0.01f);
            Invoke("StopClipper", 2f);
        }
        else if(jointOnScreen && clipper.activeSelf && clipperUsed && NoMoveAnimation())
        {
            if (Random.Range(0, 2) == 1)
            {
                InvokeRepeating("IncreaseLitTime", 0.1f, 0.1f);
                fire.transform.localScale = fireScale;
                //clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.002f);
                RotateWheel(0.2f, 0.05f);
                lastWorked = true;
            }
            else
            {
                //clipperButton.transform.localPosition = new Vector3(clipperButton.transform.localPosition.x, clipperButton.transform.localPosition.y, clipperButton.transform.localPosition.z - 0.0015f);
                RotateWheel(0.15f, 0.1f);
                lastWorked = false;
                sparks.Play();
            }
            valid = true;
        }
        else valid = false;
    }

    public bool SparkingUp()
    {
        return clipperUsed || IsInvoking("StopClipper") || IsInvoking("StopJointAndClipper");
    }

    public void StopLightClipper()
    {
        //if (jointOnScreen && clipper.activeSelf)
        //{
        if (IsInvoking("IncreaseLitTime") || valid)
        {
                if (lastWorked)
                    CancelInvoke("IncreaseLitTime");
            clipperButton.transform.localPosition = buttonStartPos;
            

        }

        timeLit = 0;
        fire.transform.localScale = Vector3.zero;
        //}

    }

    void StopJointAndClipper()
    {
        CancelInvoke("MoveJointAndClipperAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
        }
        clipperUsed = !clipperUsed;
        jointUsed = !jointUsed;
    }

    void StopJoint()
    {
        CancelInvoke("MoveJointAnimation");
        if (!jointOnScreen)
        {
            joint.SetActive(false);
        }
        jointUsed = !jointUsed;
    }

    void StopClipper()
    {
        CancelInvoke("MoveClipperAnimation");
        if (!clipperOnScreen)
        {
        }
        clipperUsed = !clipperUsed;
    }

    bool NoMoveAnimation()
    {
        return !IsInvoking("StopJointAndClipper") && !IsInvoking("StopJoint") && !IsInvoking("StopClipper");
    }

    public void ToggleJoint()
    {
        if (NoMoveAnimation())
        {
            if(jointUsed && !clipperUsed)
            {
                jointOnScreen = !jointOnScreen;
                joint.SetActive(true);
                car.ResetCamera();
                jointSpeed = -jointSpeed;
                InvokeRepeating("MoveJointAnimation", 0f, 0.01f);
                Invoke("StopJoint", 2f);
            }
            else
            {
                jointOnScreen = !jointOnScreen;
                clipperOnScreen = !clipperOnScreen;
                joint.SetActive(true);
                car.ResetCamera();
                clipperSpeed = -clipperSpeed;
                jointSpeed = -jointSpeed;
                InvokeRepeating("MoveJointAndClipperAnimation", 0f, 0.01f);
                Invoke("StopJointAndClipper", 2f);
            }
            
        }
    }

    void MoveClipperAnimation()
    {
        clipper.transform.localPosition += clipperSpeed;
    }

    void MoveJointAnimation()
    {
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
    }

    void MoveJointAndClipperAnimation()
    {
        clipper.transform.localPosition += clipperSpeed;
        joint.transform.localPosition += new Vector3(jointSpeed, -jointSpeed, jointSpeed / 10);
    }

}