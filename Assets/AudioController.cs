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

    // Start is called before the first frame update
    void Start()
    {
        soundLoopQueue = new Queue<AudioClip>();
        channels.AddRange(GetComponents<AudioSource>());
 
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
            channels[channel].clip = audioClip;
            channels[channel].Play();
    }
}
