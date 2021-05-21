using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public int squishCounter = 2;
    AudioSource audioSource;
    public AudioClip squishSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = squishSound;
        audioSource.volume = 0.5f;
    }

    public void PlaySquish()
    {
        audioSource.Play();
    }
}
