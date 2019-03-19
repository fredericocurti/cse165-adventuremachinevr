using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesizer : MonoBehaviour
{
    public Transform white_key_prefab;
    public Transform black_key_prefab;
    public Transform type_button_prefab;

    float[] white_key_freq = { 32.7f, 36.71f, 41.2f, 43.65f, 49.0f, 55.0f, 61.74f };
    float[] black_key_freq = { 34.65f, 38.89f, 46.25f, 51.91f, 58.27f };
    int whiteIndex = 0;
    int blackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        createSynth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void createSynth()
    {
        createWhiteKeys();
        createBlackKeys();
        createTypeButtons();
    }

    void createWhiteKeys()
    {
        Transform newKey;
        SynthSound synthSound = gameObject.GetComponent<SynthSound>();
        float x = -3.57f;

        for (int i = 0; i < 29; i++)
        {
            newKey = Instantiate(white_key_prefab, gameObject.transform);
            newKey.localPosition = new Vector3(x, 0.17f, -0.15f);

            KeySound thisScript = newKey.gameObject.GetComponent<KeySound>();
            thisScript.base_frequency = white_key_freq[whiteIndex];
            thisScript.white = true;
            thisScript.calcOctaveFreq((i / 7) + 2);
            synthSound.keys.Add(thisScript);

            x += 0.327f;
            if (whiteIndex == 6)
            {
                whiteIndex = 0;
            }
            else
            {
                whiteIndex++;
            }
        }
    }

    void createBlackKeys()
    {
        Transform newKey;
        SynthSound synthSound = gameObject.GetComponent<SynthSound>();
        float x = -3.4065f;

        int numKeys = 2;
        int counter = 1;
        for (int j = 0; j < 20; j++)
        {
            newKey = Instantiate(black_key_prefab, gameObject.transform);
            newKey.localPosition = new Vector3(x, 0.27f, 0.05f);

            KeySound thisScript = newKey.gameObject.GetComponent<KeySound>();
            thisScript.base_frequency = black_key_freq[blackIndex];
            thisScript.white = false;
            thisScript.calcOctaveFreq((j / 5) + 2);
            synthSound.keys.Add(thisScript);

            if (blackIndex == 4)
            {
                blackIndex = 0;
            }
            else
            {
                blackIndex++;
            }

            if (counter < numKeys)
            {
                x += 0.327f;
                counter++;
            }
            else if (numKeys == 2)
            {
                numKeys = 3;
                counter = 1;
                x += 0.654f;
            }
            else if (numKeys == 3)
            {
                numKeys = 2;
                counter = 1;
                x += 0.654f;
            }
        }
    }

    void createTypeButtons()
    {
        Transform newKey;
        SynthSound synthSound = gameObject.GetComponent<SynthSound>();
        float x = -6.0f;

        for (int k = 0; k < 4; k++)
        {
            newKey = Instantiate(type_button_prefab, gameObject.transform);
            newKey.localPosition = new Vector3(x, 0.445f, 0.71f);

            SoundType soundType = newKey.GetComponent<SoundType>();
            soundType.id = k;
            soundType.synthSound = synthSound;

            x += 0.6f;
        }
    }
}

