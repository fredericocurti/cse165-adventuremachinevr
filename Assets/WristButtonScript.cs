using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WristButtonScript : MonoBehaviour
{
    public bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("pressed button!");
        GetComponent<Image>().sprite = Resources.Load<Sprite>("menubuttoninverse");
        transform.parent.GetComponent<MenuScript>().ToggleMenu();

        if (open)
        {
            open = false;
        }
        else
        {
            open = true;
        }
    }

    public void OnDismiss()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("menubutton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
