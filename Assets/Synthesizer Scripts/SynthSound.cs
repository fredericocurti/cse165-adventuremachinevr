using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code is based on this tutorial: https://www.youtube.com/watch?v=GqHFGMy_51c
//Procedural calculation of piano sounds are based on this website: https://keithwhor.com/music/
public class SynthSound : MonoBehaviour
{
    public List<KeySound> keys = new List<KeySound>();
    public Text soundText;
    public Text octaveText;
    public float gain = 0.05f;     //The volume of the oscillator

    int type;
    int octave = 2;

    void Start()
    {
        type = 1;
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

            for (int j = 0; j < keys.Count; j++)
            {
                if(i == data.Length - channels)
                {
                    data[i] += keys[j].calculateData(type, data.Length, i, true);
                }
                else
                {
                    data[i] += keys[j].calculateData(type, data.Length, i, false);
                }
            }

            data[i] = gain * data[i];

            //copy data to second channel to make sure sound plays out of both speakers
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }

    public void changeOctave(bool increment)
    {
        if(increment && octave < 4)
        {
            octave++;
        }
        else if(!increment && octave > 1)
        {
            octave--;
        }

        Debug.Log(octave);
        octaveText.text = octave.ToString();

        for (int i = 0; i < 29; i++)
        {
            keys[i].calcOctaveFreq(i / 7 + octave);
        }

        for (int j = 29; j < 49; j++)
        {
            keys[j].calcOctaveFreq((j - 29) / 5 + octave);
        }
    }

    public void changeType(int newType)
    {
        type = newType;
        switch (type)
        {
            case 0:
                soundText.text = "Basic Sine";
                break;
            case 1:
                soundText.text = "Piano";
                break;
            case 2:
                soundText.text = "8-bit";
                break;
            case 3:
                soundText.text = "Triangle";
                break;
        }
    }
}