using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadButtonScript : MonoBehaviour
{
    public AudioClip loop;
    public bool active;
    public string loopType;
    public bool vibrating = false;
    public float vibrationTime = 0.1f;
    private string hand;
    private LaunchpadController lc;

    // Start is called before the first frame update
    void Start()
    {
        lc = GameObject.Find("Launchpad").GetComponent<LaunchpadController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name == "AvatarGrabberLeft" || other.gameObject.transform.parent.name == "AvatarGrabberRight")
        {
            hand = other.gameObject.transform.parent.name == "AvatarGrabberLeft" ? "left" : "right";
            vibrating = true;
            OVRInput.SetControllerVibration(0.3f, 1f, hand == "left" ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
            lc.onButtonPress(transform);
        }
    }

    private void OnValidate()
    {
        if (active == true)
        {
            lc.onButtonPress(transform);
            active = false;
        }
    }

    private void Update()
    {
        if (vibrating)
        {
            vibrationTime -= Time.fixedDeltaTime;
            if (vibrationTime <= 0f)
            {
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
                OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
                vibrationTime = 0.1f;
                vibrating = false;
            }
        }
        
    }
}
