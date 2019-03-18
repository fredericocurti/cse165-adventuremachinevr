using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WristButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("pressed button!");
        GetComponent<Image>().sprite = Resources.Load<Sprite>("menubuttoninverse");
        transform.parent.GetComponent<MenuScript>().ToggleMenu();
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
