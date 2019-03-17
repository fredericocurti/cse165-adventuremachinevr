using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySound : MonoBehaviour
{
    public float base_frequency;
    public float frequency;                 //the note

    float phase = 0.0f;                     //location on the wave
    float sampling_frequency = 48000.0f;    //The frequency that Unity's audio engine runs by default

    public bool pressed = false;

    void Start()
    {
    }

    void Update()
    {
        /*
        if (pressed && Input.GetKeyDown(KeyCode.Space))
        {
            gain = on_volume;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y -= 0.04f;
            gameObject.transform.localPosition = pos;
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot.x = -3.406f;
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
        if (pressed && Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0.0f;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y += 0.04f;
            gameObject.transform.localPosition = pos;
            gameObject.transform.rotation = Quaternion.identity;
        }
        */
    }

    public float calculateData()
    {
        //calculate how far to move along the x axis of the waveform
        float increment = frequency * 2.0f * Mathf.PI / sampling_frequency;

        phase += increment;
        float tempPhase = phase;

        //reset phase of waveform if it makes full revolution
        if (phase > (Mathf.PI * 2))
        {
            phase = 0.0f;
        }

        if (pressed)
        {
            return 0.5f * Mathf.Sin(tempPhase);
        }
        else
        {
            return 0.0f;
        }
    }

    public void calcOctaveFreq(int octave)
    {
        if (octave >= 1 && octave <= 7)
        {
            frequency = base_frequency * Mathf.Pow(2, octave - 1);
        }
        else if (octave == 8 && base_frequency == 32.7f)
        {
            frequency = base_frequency * Mathf.Pow(2, octave - 1);
        }
    }

    private void OnValidate()
    {
        if (pressed)
        {
            Vector3 pos = gameObject.transform.localPosition;
            pos.y -= 0.04f;
            gameObject.transform.localPosition = pos;

            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot.x = -3.406f;
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
        if (!pressed)
        {
            Vector3 pos = gameObject.transform.localPosition;
            pos.y += 0.04f;
            gameObject.transform.localPosition = pos;

            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
