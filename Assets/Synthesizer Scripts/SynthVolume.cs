using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthVolume : MonoBehaviour
{
    OVRGrabber left;
    OVRGrabber right;

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    public float value = 0.5f;
    private float prevValue = -1f;
    private SynthSound sound;
    // Start is called before the first frame update
    void Start()
    {
        left = GameObject.Find("AvatarGrabberLeft").GetComponent<OVRGrabber>();
        right = GameObject.Find("AvatarGrabberRight").GetComponent<OVRGrabber>();

        sound = GameObject.Find("Synthesizer").GetComponent<SynthSound>();
        initialRotation = transform.rotation;
        initialPosition = transform.localPosition;
    }

    void OnChange(float value)
    {
        sound.gain = value;
    }

    // Update is called once per frame
    void Update()
    {
        OVRGrabbable left_grabbed = left.grabbedObject;
        OVRGrabbable right_grabbed = right.grabbedObject;
        if (left_grabbed != null && left_grabbed.grabbedTransform != transform)
        {
            if (right_grabbed != null && right_grabbed.grabbedTransform != transform)
            {
                transform.rotation = initialRotation;
                float deltaZ = transform.localPosition.z - initialPosition.z;
                //print(deltaZ);
                if (transform.localPosition.z >= 4.6f)
                {
                    transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + 4.6f);
                }
                if (transform.localPosition.z <= -4.6f)
                {
                    transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - 4.6f);
                }

                value = (transform.localPosition.z - (-4.6f)) / (4.6f - (-4.6f));
                if (prevValue != -1 && prevValue != value)
                {
                    OnChange(value);
                }
                prevValue = value;
            }
        }

    }
}
