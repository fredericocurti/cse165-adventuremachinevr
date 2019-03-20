using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public bool menuActive = false;
    int openMenu = -1;

    Transform toggleButton;
    Transform[] children = new Transform[6];

    float timePassed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        toggleButton = transform.GetChild(0);

        for(int i = 0; i < 6; i++)
        {
            children[i] = transform.GetChild(i + 1);
            children[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(openMenu != -1)
        {
            int checkButtons = children[openMenu].GetComponent<MenuScript>().buttonPressed;
            children[openMenu].GetComponent<MenuScript>().buttonPressed = -1;
            if ( checkButtons != -1 && (Time.time - timePassed > 1.0f))
            {
                switch (openMenu)
                {
                    case 0:
                        switchMainMenu(checkButtons);
                        break;
                    case 1:
                        switchControlMenu(checkButtons);
                        break;
                    case 2:
                        locaMenuHandler();
                        break;
                }
            }

        }
    }

    public void openCloseMenu()
    {
        menuActive = !menuActive;

        //Open first menus
        if (menuActive)
        {
            timePassed = Time.time;

            openMenu = 0;
            children[openMenu].gameObject.SetActive(menuActive);
        }

        //Close all menus
        else
        {
            toggleButton.GetComponent<WristButtonScript>().OnDismiss();
            children[openMenu].gameObject.SetActive(menuActive);
            children[openMenu].GetComponent<MenuScript>().buttonPressed = -1;
            openMenu = -1;
        }
    }

    void ToggleMenu(int newMenu)
    {
        //Switch between menus
        children[openMenu].gameObject.SetActive(false);
        openMenu = newMenu;
        children[openMenu].gameObject.SetActive(menuActive);
        timePassed = Time.time;
    }

    void switchMainMenu(int buttonPressed)
    {
        switch (buttonPressed)
        {
            case 0:
                ToggleMenu(1);
                break;

            case 1:
                ToggleMenu(2);
                break;
        }
    }

    void switchControlMenu(int buttonPressed)
    {
        switch (buttonPressed)
        {
            case 0:
                ToggleMenu(3);
                break;

            case 1:
                ToggleMenu(4);
                break;

            case 2:
                ToggleMenu(5);
                break;
        }
    }

    void locaMenuHandler()
    {
        print("change locations");
    }
}
