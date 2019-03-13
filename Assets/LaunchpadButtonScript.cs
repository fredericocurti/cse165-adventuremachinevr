using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadButtonScript : MonoBehaviour
{
    public AudioClip loop;
    public bool active;
    public string loopType;

    private LaunchpadController lc;

    // Start is called before the first frame update
    void Start()
    {
        lc = GameObject.Find("Launchpad").GetComponent<LaunchpadController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        lc.onButtonPress(transform);
    }

    private void OnValidate()
    {
        if (active == true)
        {
            lc.onButtonPress(transform);
            active = false;
        }
    }

    // Update is called once per frame
}
