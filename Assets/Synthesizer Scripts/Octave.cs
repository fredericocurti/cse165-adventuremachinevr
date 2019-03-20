using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octave : MonoBehaviour
{
    SynthSound synthSound;
    float pressedTime = 0.0f;
    Vector3 currPos;

    // Start is called before the first frame update
    void Start()
    {
        synthSound = gameObject.transform.parent.GetComponent<SynthSound>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if(Time.time - pressedTime > 0.5f)
        {
            currPos = transform.localPosition;
            transform.localPosition = new Vector3(currPos.x, currPos.y - 0.04f, currPos.z);

            if (gameObject.name == "Octave_button_dec")
            {
                synthSound.changeOctave(false);
            }
            else
            {
                synthSound.changeOctave(true);
            }

            pressedTime = Time.time;
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
