using UnityEngine.Audio;
using System;
using UnityEngine;

public class AuidoManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip main;
    public AudioClip lost;
    public AudioClip won;

    void Start()
    {
        source.Play();
    }

    void Update()
    {

    }
}