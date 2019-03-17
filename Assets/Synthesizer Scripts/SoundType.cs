using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundType : MonoBehaviour
{
    public int id;
    public bool pressed;
    public SynthSound synthSound;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }

    private void OnValidate()
    {
        if (pressed)
        {
            synthSound.type = id;
            pressed = false;
        }
    }
}
