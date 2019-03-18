using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public bool menuActive = false;
    Transform toggleButton;
    Transform menuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        toggleButton = transform.GetChild(0);
        menuCanvas = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        menuActive = !menuActive;
        if (menuActive == false)
        {
            toggleButton.GetComponent<WristButtonScript>().OnDismiss();
            menuCanvas.gameObject.SetActive(false);
        } else
        {
            menuCanvas.gameObject.SetActive(true);
        }
    }

    public void OnPressItem()
    {
        print("onpressitem");
    }
}
