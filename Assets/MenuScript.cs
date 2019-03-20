using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public int buttonPressed = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetChild(0).gameObject.name != "Text")
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<ButtonScript>().pressed == true)
                {
                    buttonPressed = i;
                    transform.GetChild(i).GetComponent<ButtonScript>().pressed = false;
                    break;
                }
            }
        }
    }


}
