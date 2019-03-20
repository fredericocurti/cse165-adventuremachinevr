using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundType : MonoBehaviour
{
    public int id;
    public SynthSound synthSound;
    Vector3 currPos;

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
            currPos = transform.localPosition;
            transform.localPosition = new Vector3(currPos.x, currPos.y - 0.04f, currPos.z);
            synthSound.changeType(id);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.transform.parent.name == "AvatarGrabberLeft" || collision.gameObject.transform.parent.name == "AvatarGrabberRight")
        {
            transform.localPosition = currPos;
        }
    }
}
