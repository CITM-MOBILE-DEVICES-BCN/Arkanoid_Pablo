using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Musicmanager : MonoBehaviour
{
    public static Musicmanager instance;
    public Sound[] sounds;
    public AudioSource soundssource;




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound: not found!");
            return;
        }
        else
        {
            soundssource.clip = s.clip;
            soundssource.Play();
        }

    }
}
