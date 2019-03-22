using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristButtonScript : MonoBehaviour
{
    public float debounceTime = 1f;
    public bool ready = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ready)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("menubuttoninverse");
            transform.parent.GetComponent<MainMenuScript>().openCloseMenu();
        }
        ready = false;

    }

    public void OnDismiss()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("menubutton");
    }

    // Update is called once per frame
    void Update()
    {
        if (ready == false)
        {
            debounceTime -= Time.deltaTime;
        }

        if (debounceTime <= 0f)
        {
            ready = true;
            debounceTime = 2f;
        }
    }
}
