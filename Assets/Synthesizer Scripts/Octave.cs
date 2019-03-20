using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octave : MonoBehaviour
{
    SynthSound synthSound;
    float pressedTime = 0.0f;

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
        if(Time.time - pressedTime > 1.0f)
        {
            if(gameObject.name == "Octave_button_dec")
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
}
