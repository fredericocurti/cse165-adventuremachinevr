using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySound : MonoBehaviour
{
    public float base_frequency;
    public float frequency;                 //the note

    float phase = 0.0f;                     //location on the wave
    float beta = 0.0f;
    float sampling_frequency = 48000.0f;    //The frequency that Unity's audio engine runs by default
    int counter = 0;
    int hold = 0;

    public bool pressed = false;

    void Start()
    {
    }

    void Update()
    {
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

    public float calculateData(int type, int dataSize, int i, bool lastCall)
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

        switch (type)
        {
            case 0:
                return calculateSine(tempPhase);
            case 1:
                return calculatePiano(tempPhase, dataSize, i, lastCall);
            case 2:
                return calculate8Bit(tempPhase);
            case 3:
                return calculateTriangle(tempPhase);
            default:
                return 0.0f;
        }
    }

    float calculateSine(float phase)
    { 
        return beta * Mathf.Sin(phase);
    }

    float calculatePiano(float phase, int dataSize, int i, bool lastCall)
    {
        float attack = 0.002f;
        float currVol;

        if (!pressed)
        {
            return 0.0f;
        }

        if (counter < 3)
        {
            currVol = beta * ( ((dataSize * counter) + i) / (sampling_frequency * 0.128f) );
        }
        else if (counter >= 3 && counter < 55)
        {
            float dampen = Mathf.Pow(Mathf.Log(frequency * beta * 15.3f), 2);
            currVol = beta * Mathf.Pow(1 - ( ((dataSize * (counter - 2)) + (i - dataSize + 2)) / (sampling_frequency * 20.0f) ), dampen);
        }
        else
        {
            pressed = false;
            currVol = 0.0f;
        }

        float y_val = Mathf.Pow(Mathf.Sin(phase), 2);
        y_val += (0.1f * Mathf.Sin(phase + (2.0f * 0.5f * Mathf.PI)));
        y_val += (0.75f * Mathf.Sin(phase + (2.0f * 0.25f * Mathf.PI)));
        y_val = Mathf.Sin(phase + y_val);
        y_val = Mathf.Min(Mathf.Max(y_val, -1), 1);

        if (lastCall && counter == 3 && hold == 3)
        {
            counter++;
        }
        else if(lastCall && counter != 3)
        {
            counter++;
        }
        else if(lastCall && hold < 5)
        {
            hold++;
        }

        return currVol * y_val;
    }

    float calculate8Bit(float phase)
    {
        if (0.5f * Mathf.Sin(phase) >= 0)
        {
            return beta * 0.6f;
        }
        else
        {
            return -beta * 0.6f;
        }
    }

    float calculateTriangle(float phase)
    {
        return beta * Mathf.PingPong(phase, 1.0f);
    }

    private void OnValidate()
    {
        if (pressed)
        {
            beta = 0.5f;
            counter = 0;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y -= 0.04f;
            gameObject.transform.localPosition = pos;

            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot.x = -3.406f;
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
        if (!pressed)
        {
            beta = 0.0f;
            hold = 0;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y += 0.04f;
            gameObject.transform.localPosition = pos;

            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
