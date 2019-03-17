using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code is based on this tutorial: https://www.youtube.com/watch?v=GqHFGMy_51c
//Procedural calculation of piano sounds are based on this website: https://keithwhor.com/music/
public class SynthSound : MonoBehaviour
{
    public float base_frequency;

    float frequency;                        //the note
    float gain;                             //The volume of the oscillator

    float sampling_frequency = 48000.0f;    //The frequency that Unity's audio engine runs by default
    float on_volume = 0.05f;                //"on" volume. Volumes higher than 1.0 run risk of damaging speakers and ears.

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gain = on_volume;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0.0f;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        //calculate how far to move along the x axis of the waveform
        float increment = frequency * 2.0f * Mathf.PI / sampling_frequency;

        //location on the wave
        float phase = 0.0f;

        //iterate through data to set position on y axis of waveform
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = gain * Mathf.Sin(phase);

            //copy data to second channel to make sure sound plays out of both speakers
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            //reset phase of waveform if it makes full revolution
            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0f;
            }
        }
    }

    public void calcOctaveFreq(int octave)
    {
        if(octave >= 1 && octave <= 4)
        {
            frequency = base_frequency * Mathf.Pow(2, octave - 1);
        }
    }
}