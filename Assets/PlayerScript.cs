using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform rightHand;
    public GameObject leftController;
    public Transform glowRing;
   
    // Start is called before the first frame update
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


    }
}
