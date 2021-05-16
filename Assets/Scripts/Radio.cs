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
    public AudioClip[] radioStations;
    public AudioClip[] freestyleBeats;
    readonly bool shuffleBeatsEveryLoop = false; //Change frestyle beats order every loop finish
    int freestyleBeatIndex = 0;
    int stationIndex = 0;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.VolUp.performed += ctx => RepeatVolUp();
        controls.Gameplay.VolUp.canceled += ctx => CancelInvoke("VolUp");

        controls.Gameplay.VolDown.performed += ctx => RepeatVolDown();
        controls.Gameplay.VolDown.canceled += ctx => CancelInvoke("VolDown");

        controls.Gameplay.NextStation.performed += ctx => NextStation();
        controls.Gameplay.PreviousStation.performed += ctx => PreviousStation();
    }

    void Start()
    {
        playerCar = GetComponent<PlayerCar>();
        ShuffleArray(freestyleBeats);
        radio.volume = 0.5f;
        float maxLength = 0f;
        for (int i = 0; i < radioStations.Length; i++)
            if (radioStations[i].length > maxLength)
                maxLength = radioStations[i].length;
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
        radio.loop = true;
        if (index != radioStations.Length)
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
        radioText.text = radioStationsNames[index];
        radioText.font = radioStationsFonts[index];
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
}
