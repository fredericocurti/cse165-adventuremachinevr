using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octave : MonoBehaviour
{
    public bool pressed;
    SynthSound synthSound;

    // Start is called before the first frame update
    void Start()
    {
        synthSound = gameObject.transform.parent.GetComponent<SynthSound>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
        if (pressed)
        {
            if(gameObject.name == "Octave_button_dec")
            {
                synthSound.changeOctave(false);
            }
            else
            {
                synthSound.changeOctave(true);
            }
            pressed = false;
        }
    }
}
