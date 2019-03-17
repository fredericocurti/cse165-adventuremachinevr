﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code is based on this tutorial: https://www.youtube.com/watch?v=GqHFGMy_51c
//Procedural calculation of piano sounds are based on this website: https://keithwhor.com/music/
public class SynthSound : MonoBehaviour
{
    public List<KeySound> keys = new List<KeySound>();
    float gain = 0.05f;     //The volume of the oscillator


    void Start()
    {
    }

    void Update()
    {
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        //iterate through data to set position on y axis of waveform
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = 0.0f;
            for(int j = 0; j < keys.Count; j++)
            {
                data[i] += keys[j].calculateData();
            }

            data[i] = gain * data[i];

            //copy data to second channel to make sure sound plays out of both speakers
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
}