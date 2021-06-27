using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{

    PlayerCar playerCar;
    PlayerControls controls;
    public AudioSource radio;
    [SerializeField] string[] radioStationsNames;
    [SerializeField] Font[] radioStationsFonts;
    [SerializeField] Text radioText;
    [SerializeField] Text clockText;
    Vector3 baseClockScale;
    [SerializeField] LightCycle lightCycle;
    public Text speedText;
    public AudioClip[] radioStations;
    [SerializeField] AudioClip respectClip;
    float[] stationsDelays;
    public AudioClip[] freestyleBeats;
    readonly bool shuffleBeatsEveryLoop = false; //Change frestyle beats order every loop finish
    int freestyleBeatIndex = 0;
    int stationIndex = 0;
    float clockTime = 0f;
    public static float minuteTime = 1f;
    Weed joint;
    [SerializeField] Material moonMaterial;
    [SerializeField] GameObject moon;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.VolUp.performed += ctx => RepeatVolUp();
        controls.Gameplay.VolUp.canceled += ctx => CancelInvoke("VolUp");

        controls.Gameplay.VolDown.performed += ctx => RepeatVolDown();
        controls.Gameplay.VolDown.canceled += ctx => CancelInvoke("VolDown");

        controls.Gameplay.NextStation.performed += ctx => NextStation();
        controls.Gameplay.PreviousStation.performed += ctx => PreviousStation();

        controls.Gameplay.ToggleHUD.performed += ctx => ToggleHUD();
    }

    void Start()
    {
        playerCar = GetComponent<PlayerCar>();
        joint = FindObjectOfType<Weed>();
        //clockTime = Random.Range(0, 1441);
        clockTime = 252f;
        baseClockScale = clockText.transform.localScale;
        SetClock();
        InvokeRepeating("AddMinute", 0f, minuteTime);
        ShuffleArray(freestyleBeats);
        radio.volume = 0.25f;
        float maxLength = 0f;
        for (int i = 0; i < radioStations.Length; i++)
            if (radioStations[i].length > maxLength)
                maxLength = radioStations[i].length;
        stationsDelays = new float[radioStations.Length];
        for (int i = 0; i < stationsDelays.Length; i++)
        {
            stationsDelays[i] = Random.Range(0, maxLength);
        }
        playerCar.TimeNow = Random.Range(0, maxLength);
        PlayStation(stationIndex, playerCar.TimeNow);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void RepeatVolUp()
    {
        if (!IsInvoking("VolUp") && !IsInvoking("VolDown") && !fadingOut)
            InvokeRepeating("VolUp", 0f, 0.1f);
    }

    void RepeatVolDown()
    {
        if (!IsInvoking("VolUp") && !IsInvoking("VolDown") && !fadingOut)
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
        PlayStation(stationIndex, playerCar.TimeNow);
    }

    void PreviousStation()
    {
        if (stationIndex > 0)
            stationIndex--;
        else
            stationIndex = radioStations.Length;
        PlayStation(stationIndex, playerCar.TimeNow);
    }

    void PlayStation(int index, float time)
    {
        if (respecting)
            return;
        radio.loop = true;
        if (index != radioStations.Length)
        {
            float stationLength = radioStations[stationIndex].length;
            radio.time = 0;
            radio.clip = radioStations[index];
            if (time + stationsDelays[index] < stationLength)
                radio.time = time;
            else
            {
                float timeCopy = time + stationsDelays[index];
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

        if (radioText.text.Equals("Haknufia"))
            radio.volume /= 2;

        radioText.text = radioStationsNames[index];
        radioText.font = radioStationsFonts[index];

        if (radioText.text.Equals("Haknufia"))
            radio.volume *= 2;
        //radioText.fontStyle = FontStyle.Bold;
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

    public void CheckForNextFreestyleBeat()
    {
        if (!radio.loop && !radio.isPlaying)
        {
            if (shuffleBeatsEveryLoop && freestyleBeatIndex >= freestyleBeats.Length - 1)
                ShuffleArray(freestyleBeats);
            PlayStation(stationIndex, playerCar.TimeNow);
        }
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
    }

    void Update()
    {
        if ((clockTime == 257 || clockTime == 977) && !fadingOut)
            StartFadeOut();
        if (clockTime == 260 || clockTime == 980)
        {
            clockText.enabled = true;
            if(!respecting && !wasOnThis420)
                PlayRespect();
        }
        else clockText.enabled = radioText.enabled;
        if (clockTime == 261 || clockTime == 981)
            wasOnThis420 = false;
        if (respecting)
            radio.volume = 1f;
    }

    float oldVolume = 1f;
    bool respecting = false, wasOnThis420 = false;

    float volDec;
    bool fadingOut = false;
    void StartFadeOut()
    {
        fadingOut = true;
        oldVolume = radio.volume;
        volDec = radio.volume / 30f;
        InvokeRepeating("DecVol", 0.1f, 0.1f);
        Invoke("FadeOutEnd", 3f);
    }
    void DecVol()
    {
        radio.volume -= volDec;
    }

    void FadeOutEnd()
    {
        CancelInvoke("DecVol");
        fadingOut = false;
    }
    void PlayRespect()
    {
        if(!CinematicMode.active)
            FindObjectOfType<Weed>().SetJointStuffIfNotSet();
        wasOnThis420 = true;
        respecting = true;
        radio.Stop();
        radio.volume = 1f;
        radio.time = 0f;
        radio.clip = respectClip;
        radio.Play();
        Invoke("RespectEnd", 7f);
    }

    void RespectEnd()
    {
        respecting = false;
        radio.Stop();
        radio.volume = oldVolume;
        PlayStation(stationIndex, playerCar.TimeNow);
    }

    static float counter = 138;
    float step = 0.0035f;
    float timeToChange = 0.125f;
    void AddMinute()
    {
        UpdateMoonAlpha();
        if (IsInvoking("IncreaseClockSize"))
            CancelInvoke("IncreaseClockSize");
        else if (IsInvoking("DecreaseClockSize"))
            CancelInvoke("DecreaseClockSize");

        float dayMinutes = 1440;
        clockTime += 1f;
        if (clockTime >= dayMinutes)
            clockTime = 0;
       if((clockTime >= 240 && clockTime < 260) || (clockTime >= 960 && clockTime < 980))
        {
            /*if (!IsInvoking("Greener"))
            {
                if (clockTime >= 240 && clockTime < 260)
                    greenerCounter -= 260 - clockTime;
                else
                    greenerCounter -= 980 - clockTime;
                clockText.color = new Color(clockText.color.r - (20 - greenerCounter) * otherStep, clockText.color.g - (20 - greenerCounter) * greenStep, clockText.color.b - (20 - greenerCounter) * otherStep);
                if (clockText.color.r > 1f - otherStep)
                    clockText.color = new Color(0f, clockText.color.g, clockText.color.b);
                if (clockText.color.b > 1f - otherStep)
                    clockText.color = new Color(clockText.color.r, clockText.color.g, 0f);
                InvokeRepeating("Greener", minuteTime, minuteTime);
            }*/
        }
        else if (clockTime == 260 || clockTime == 980)
        {
            float _420time = 69f;
            if(CinematicMode.active)
                joint.AutoSmoke(_420time);
            clockText.color = new Color(0f, 0.5f, 0f);
            InvokeRepeating("IncreaseClockSize", timeToChange, timeToChange);
            CancelInvoke("AddMinute");
            InvokeRepeating("AddMinute", _420time, minuteTime);
        }
        else
        {
            clockText.color = new Color(245f / 255f, 226f / 255f, 223f / 255f);
            clockText.transform.localScale = baseClockScale;
        }
        SetClock();
        lightCycle.UpdateLight(clockTime);
    }

  

    void UpdateMoonAlpha()
    {
        //moonMaterial.color = new Color(255f, 255f, 255f, 0f);

        //1140 - 7PM, 270 - 4:30AM
        if (clockTime > 1140 || clockTime < 270)
        {
            moon.SetActive(true);
            float alpha = 0f;
            float num;
            if (clockTime > 1140)
                num = clockTime - 1140;
            else num = 300 + clockTime;
            //middle : num = 285
            float difference = Mathf.Abs(285 - num);
            alpha = 1f - difference / 500f;

            moonMaterial.color = new Color(1f, 1f, 1f, alpha);
        }
        else moon.SetActive(false);

    }
    
    void IncreaseClockSize()
    {
        clockText.transform.localScale += new Vector3(step, step, step);
        counter--;
        if(counter <= 0)
        {
            CancelInvoke("IncreaseClockSize");
            InvokeRepeating("DecreaseClockSize", timeToChange, timeToChange);
        }    
    }

    public bool Is420()
    {
        return clockTime == 260 || clockTime == 980;
    }

    void DecreaseClockSize()
    {
        clockText.transform.localScale -= new Vector3(step, step, step);
        counter++;
        if (counter >= 138)
        {
            CancelInvoke("DecreaseClockSize");
            InvokeRepeating("IncreaseClockSize", timeToChange, timeToChange);
        }
    }



    void SetClock()
    {
        float hours = (int)clockTime / 60;
        float minutes = clockTime - hours * 60;
        string hoursString, minutesString;
        if (hours < 10)
            hoursString = "0" + hours;
        else hoursString = hours.ToString();

        if (minutes < 10)
            minutesString = "0" + minutes;
        else minutesString = minutes.ToString();

        clockText.text = hoursString + ":" + minutesString;
    }

    void ToggleHUD()
    {
        Text[] hudTexts = {radioText, clockText, speedText };
        foreach (Text hudText in hudTexts)
            hudText.enabled = !hudText.enabled;
    }
}
