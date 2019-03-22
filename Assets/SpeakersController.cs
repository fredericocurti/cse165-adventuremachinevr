using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakersController : MonoBehaviour { 
    public AudioClip bassLoop;
    public AudioClip drumLoop;
    public Queue<AudioClip> soundLoopQueue;
    public GameObject[] speakerGameObjects;
    public List<AudioSource> speakerAudioSources;
    private int channelsPerSpeaker = 5;

    // Start is called before the first frame update
    void Start()
    {
        soundLoopQueue = new Queue<AudioClip>();
        speakerGameObjects = GameObject.FindGameObjectsWithTag("Speaker");
        for (int i = 0; i < speakerGameObjects.Length; i++)
        {
            speakerAudioSources.AddRange(speakerGameObjects[i].GetComponents<AudioSource>());
        }
    }

    public void PlayLoop(AudioClip audioClip, string loopType)
    {
        if (loopType == "bass" && audioClip != bassLoop)
        {
            bassLoop = audioClip;
            PlayClipOnChannel(audioClip, 4);
        }
        if (loopType == "drum" && audioClip != bassLoop)
        {
            drumLoop = audioClip;
            PlayClipOnChannel(audioClip, 3);
        }

    }



    void PlayClipOnChannel(AudioClip audioClip, int channel)
    {
        for (int i = 0; i < speakerGameObjects.Length; i++)
        {
            speakerAudioSources[i].clip = audioClip;
            //speakerAudioSources[i + channelsPerSpeaker].clip = audioClip;

            //speakerAudioSources[i + channelsPerSpeaker].Play();
            speakerAudioSources[i].Play();
        }

    }
}
