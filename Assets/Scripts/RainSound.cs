using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSound : MonoBehaviour
{
    public AudioSource audioPlayer;

    void Start()
    {
        audioPlayer.Play();
    }
}
