using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AudioController : MonoBehaviour
{
    public AudioClip bassLoop;
    public AudioClip drumLoop;
    public Queue<AudioClip> soundLoopQueue;
    public GameObject[] speakerGameObjects;
    public List<AudioSource> channels;
    private int channelsPerSpeaker = 5;
    private bool masterPlaying = false;

    // Start is called before the first frame update

    private void Awake()
    {
        channels.AddRange(GetComponents<AudioSource>());
    }

    public void PlayLoop(AudioClip audioClip, string loopType)
    {
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
        if (masterPlaying == false)
        {
            masterPlaying = true;
            channels[0].Play();
        }
        if (channels[channel].clip != audioClip)
        {
            channels[channel].clip = audioClip;
            channels[channel].Play();
            //channels[channel].PlayOneShot(audioClip);
            //}

        }
    }

    public void ClearChannel(int channel)
    {
        channels[channel].Stop();
        channels[channel].clip = null;
    }

    private void Update()
    {
        for (int i = 0; i < channelsPerSpeaker; i++)
        {
            channels[i + 1].timeSamples = channels[0].timeSamples;
        }
    }
}
