using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderScript : MonoBehaviour
{
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    public float value = 0.5f;
    private float prevValue = -1f;
    private AudioController ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = GameObject.Find("AudioPlayer").GetComponent<AudioController>();
        initialRotation = transform.rotation;
        initialPosition = transform.localPosition;
    }

    void OnChange(float value)
    {
        ac.master.volume = value;
    }

    // Update is called once per frame
    void Update()
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
