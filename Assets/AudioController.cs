using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AudioController : MonoBehaviour
{
    public AudioClip bassLoop;
    public AudioClip drumLoop;
    public Queue<AudioClip> soundLoopQueue;
    public GameObject[] speakerGameObjects;
    public AudioSource[] channels;
    public AudioSource master;
    private int channelsPerSpeaker = 5;
    private bool masterPlaying = false;
    private GameObject listener;
    public AudioLowPassFilter lowPassFilter;
    public AudioHighPassFilter highPassFilter;
    public AudioReverbFilter reverbFilter;
    public CircularSliderScript lowPassDial;
    public CircularSliderScript highPassDial;
    public CircularSliderScript reverbDial;
    public bool inSync = false;

    // Start is called before the first frame update

    private void Awake()
    {
        lowPassDial = GameObject.Find("LowPass").GetComponent<CircularSliderScript>();
        highPassDial = GameObject.Find("HighPass").GetComponent<CircularSliderScript>();
        reverbDial = GameObject.Find("Reverb").GetComponent<CircularSliderScript>();
        channels = GetComponents<AudioSource>();
        master = channels[0];
    }

    private void Start()
    {
        listener = GameObject.Find("CenterEyeAnchor");
        lowPassFilter = listener.GetComponent<AudioLowPassFilter>();
        highPassFilter = listener.GetComponent<AudioHighPassFilter>();
        reverbFilter = listener.GetComponent<AudioReverbFilter>();
    }

    public void PlayLoop(AudioClip audioClip, string loopType)
    {
        inSync = false;
        if (loopType == "bass")
        {
            bassLoop = audioClip;
            PlayClipOnChannel(audioClip, 5);
        }
        if (loopType == "drum")
        {
            drumLoop = audioClip;
            PlayClipOnChannel(audioClip, 4);
        }
    }


    public void PlayClipOnChannel(AudioClip audioClip, int channel)
    {
        inSync = false;
        if (masterPlaying == false)
        {
            masterPlaying = true;
            master.Play();
            //StartCoroutine(SyncSources());
        }

        if (channels[channel].clip != audioClip)
        {
            channels[channel].clip = audioClip;
            channels[channel].Play();
        }
    }

    public void ClearChannel(int channel)
    {
        channels[channel].Stop();
        channels[channel].clip = null;
    }



    private void Update()
    {
        for (int i = 1; i < channelsPerSpeaker + 1; i++)
        {
            if (inSync == false)
            {
                channels[i].timeSamples = master.timeSamples;
            }
            channels[i].volume = master.volume;
            channels[i].pitch = master.pitch;
        }

        if (inSync == false)
        {
            inSync = true;
        }

        lowPassFilter.cutoffFrequency = 10f + (21990f * lowPassDial.value);
        highPassFilter.cutoffFrequency = 22000f - (22000f * highPassDial.value);
        reverbFilter.reverbLevel = -1000f + (3000f * reverbDial.value);

        //float[] spectrum = new float[256];

        //AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        //for (int i = 1; i < spectrum.Length - 1; i++)
        //{
        //    Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
        //    Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        //}

    }

    private IEnumerator SyncSources()
    {
        while (true)
        {
            for (int i = 1; i < channelsPerSpeaker + 1; i++)
            {
                channels[i].timeSamples = master.timeSamples;
                yield return null;
            }
        }
    }
}
