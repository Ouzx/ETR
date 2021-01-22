using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio.loop = true;
        audio.Play();

    }

}
