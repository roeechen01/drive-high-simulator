using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    float cameraSpeed = 400f;
    PlayerControls controls;
    Vector2 direction;
    Vector2 view;
    float gasAmount;
    float reverseAmount;
    public bool build = false;
    public float buildDifference = 5.5f;
    public AudioSource radio;
    public AudioSource engine;
    float time = 0f;
    public AudioClip[] radioStations;
    public AudioClip[] freestyleBeats;
    readonly bool shuffleBeatsEveryLoop = false; //Change frestyle beats order every loop finish
    int freestyleBeatIndex = 0;
    int stationIndex = 0;

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

        controls.Gameplay.VolUp.performed += ctx => RepeatVolUp();
        controls.Gameplay.VolUp.canceled += ctx => CancelInvoke("VolUp");

        controls.Gameplay.VolDown.performed += ctx => RepeatVolDown();
        controls.Gameplay.VolDown.canceled += ctx => CancelInvoke("VolDown");

        controls.Gameplay.NextStation.performed += ctx => NextStation();
        controls.Gameplay.PreviousStation.performed += ctx => PreviousStation();

        controls.Gameplay.Restart.performed += ctx => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        ShuffleArray(freestyleBeats);
        myRigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<Camera>();
        InvokeRepeating("Timer", 0f, tick);
        radio.volume = 0.5f;
        float maxLength = 0f;
        for (int i = 0; i < radioStations.Length; i++)
            if (radioStations[i].length > maxLength)
                maxLength = radioStations[i].length;
        time = Random.Range(0, maxLength);
        PlayStation(stationIndex, time);



        myCamera.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.8f, transform.position.z - 0.6f);
        InvokeRepeating("CheckPedals", 0.1f, 0.1f);

        if (build)
            speedAddition /= buildDifference;
        else buildDifference = 1;
    }

    void CheckPedals()
    {
        float reverseMax = -7500f / buildDifference, gasMax = 15000f / buildDifference, noGasDiv = 15f;

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

        if (currentSound < 0)
            currentSound = 0;
        else if (currentSound > 100)
            currentSound = 100;
        //if (currentSound <= soundAddition && Mathf.Abs(currentSpeed) > 0)
        // currentSound = (Mathf.Abs(currentSpeed) / -1 * reverseMax) * 100;
        engine.volume = 0.8f * (currentSound / 100);
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
        myRigidbody.velocity = transform.forward * currentSpeed * Time.deltaTime;
    }

    void ChangeCameraDirection()
    {
        if (view != Vector2.zero)
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

    void RepeatVolUp()
    {
        if (!IsInvoking("VolUp") && !IsInvoking("VolDown"))
            InvokeRepeating("VolUp", 0f, 0.1f);
    }

    void RepeatVolDown()
    {
        if (!IsInvoking("VolUp") && !IsInvoking("VolDown"))
            InvokeRepeating("VolDown", 0f, 0.1f);
    }

    void VolUp()
    {
        radio.volume += 0.05f;
        if (radio.volume > 1)
            radio.volume = 1;
    }

    void VolDown()
    {
        radio.volume -= 0.05f;
        if (radio.volume < 0)
            radio.volume = 0;
    }

    void NextStation()
    {
        if (stationIndex == radioStations.Length)
            stationIndex = 0;
        else if (stationIndex + 1 < radioStations.Length)
            stationIndex++;
        else
            stationIndex = radioStations.Length;
        PlayStation(stationIndex, time);
    }

    void PreviousStation()
    {
        if (stationIndex > 0)
            stationIndex--;
        else
            stationIndex = radioStations.Length;
        PlayStation(stationIndex, time);
    }

    void PlayStation(int index, float time)
    {
        radio.loop = true;
        if(index != radioStations.Length)
        {
            float stationLength = radioStations[stationIndex].length;
            radio.time = 0;
            radio.clip = radioStations[index];
            if (time < stationLength)
                radio.time = time;
            else
            {
                float timeCopy = time;
                while (timeCopy > stationLength)
                    timeCopy -= stationLength;
                radio.time = timeCopy;
            }
            radio.Play();
        }
        else
        {
            float stationLength = 0f;
            foreach (AudioClip freestyleBeat in freestyleBeats)
                stationLength += freestyleBeat.length;
            radio.time = 0;

            if (time < stationLength)
                FakeFreestyleBeatsStation(time);
            else
            {
                float timeCopy = time;
                while (timeCopy > stationLength)
                    timeCopy -= stationLength;
                FakeFreestyleBeatsStation(timeCopy);
            }
            radio.Play();
        }
        
    }

    void FakeFreestyleBeatsStation(float time)
    {
        radio.loop = false;
        float timeLeft = time;
        bool flag = true;
        int index = 0;
        while (flag)
        {
            if (index >= freestyleBeats.Length)
                index = 0;
            if (timeLeft < freestyleBeats[index].length)
            {
                freestyleBeatIndex = index;
                flag = false;
                radio.clip = freestyleBeats[index];
                radio.time = timeLeft;

            }
            timeLeft -= freestyleBeats[index].length;
            index++;
        }
    }

    void CheckForNextFreestyleBeat()
    {
        if(!radio.loop && !radio.isPlaying) {
            if (shuffleBeatsEveryLoop && freestyleBeatIndex >= freestyleBeats.Length - 1)
                ShuffleArray(freestyleBeats);
            PlayStation(stationIndex, time);
        }
    }

    void Timer()
    {
        time += tick;
        CheckForNextFreestyleBeat();
    }

    public void ShuffleArray(AudioClip[] arr)
    {
        AudioClip other;
        for (int i = 0; i < arr.Length; i++)
        {
            int rnd = Random.Range(0, arr.Length);
            other = arr[rnd];
            arr[rnd] = arr[i];
            arr[i] = other;
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
}
