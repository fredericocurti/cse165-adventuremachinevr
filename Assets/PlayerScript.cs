using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform rightHand;
    public GameObject leftController;
    public Transform glowRing;
    public Vector3 targetPosition;
    public float smoothFactor = 10;
    public bool grabbingTheAir = false;
    //public float grabbingTheAirSpeedFactor = 100f;
    private Vector3 prevPos;
    private GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        GlobalControl gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalControl>();
        transform.position = gc.prevPlayerPosition;
        transform.rotation = gc.prevPlayerRotation;
    }


    void Start()
    {
        leftController = GameObject.Find("controller_left");
        glowRing.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        bool floorHit = false;
        Vector3 leftIndexPosition = leftController.transform.TransformPoint(-0.045f, 0f, 0.1f);
        Debug.DrawRay(leftIndexPosition, leftController.transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(leftIndexPosition, leftController.transform.forward, out raycastHit, 5f))
        {
            if (raycastHit.collider.gameObject.tag == "Terrain")
            {
                floorHit = true;
                if (!glowRing.gameObject.activeSelf)
                {
                    glowRing.gameObject.SetActive(true);
                }
                glowRing.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.05f, raycastHit.point.z);
            }
        }

        if (!floorHit)
        {
            glowRing.gameObject.SetActive(false);
        }

        if (floorHit && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            transform.position = glowRing.transform.position;
        }


        // Get pinch for grab the air
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && grabbingTheAir == false) {
            grabbingTheAir = true;
        } else
        {
            grabbingTheAir = false;
        }

        if (grabbingTheAir)
        {
            Vector3 rightTouchVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTrackedRemote);
            targetPosition = new Vector3(rightTouchVelocity.x, 0f, rightTouchVelocity.z) * -1f;
            transform.position = Vector3.Lerp(transform.position, transform.position + targetPosition, Time.fixedDeltaTime * smoothFactor);
        }
    }
}
