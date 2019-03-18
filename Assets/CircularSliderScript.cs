using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularSliderScript : MonoBehaviour
{
    private Vector3 initialRotation;
    private Vector3 prevRotation;
    private Transform dial;
    private Image visualizer;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        dial = transform.Find("Dial");
        visualizer = transform.Find("Visualizer").GetComponent<Image>();
        initialRotation = dial.localEulerAngles;
        prevRotation = initialRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //float deltaRot = initialRotation.eulerAngles.y - transform.rotation.eulerAngles.y;
        //print(dial.localRotation.eulerAngles.y);

        //print(dial.localEulerAngles.y);
        //if (dial.localEulerAngles.y > 355f)
        //{
        //    dial.localRotation = Quaternion.Euler(new Vector3(dial.localEulerAngles.x, 358.9f, dial.localEulerAngles.z));
        //}
        value = dial.localEulerAngles.y / 360f;
        visualizer.fillAmount = value;
    }

}
