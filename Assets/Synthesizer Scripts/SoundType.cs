using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundType : MonoBehaviour
{
    public int id;
    public SynthSound synthSound;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.parent.name == "AvatarGrabberLeft" || collision.gameObject.transform.parent.name == "AvatarGrabberRight")
        {
            synthSound.changeType(id);
        }
    }
}
