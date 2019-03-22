using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    OVRGrabber left;
    OVRGrabber right;

    public Tutorial tutorial;
    public bool menuActive = false;
    public bool grab = false;

    Transform toggleButton;
    Transform[] children = new Transform[6];

    int openMenu = -1;
    float timePassed = 0.0f;
    public bool tutorialOff = false;

    private void Awake()
    {
        GameObject globalControl = GameObject.FindGameObjectWithTag("GameController");
        if (globalControl.GetComponent<GlobalControl>().reset)
        {
            GameObject.Find("Tutorial Text").GetComponent<Tutorial>().turnOff();
            GameObject.Find("Tutorial_On").SetActive(false);
            tutorialOff = true;
        }
        else
        {
            tutorial = GameObject.Find("Tutorial Text").GetComponent<Tutorial>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        left = GameObject.Find("AvatarGrabberLeft").GetComponent<OVRGrabber>();
        right = GameObject.Find("AvatarGrabberRight").GetComponent<OVRGrabber>();

        toggleButton = transform.GetChild(0);

        for(int i = 0; i < 5; i++)
        {
            children[i] = transform.GetChild(i + 1);
            children[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialOff && tutorial.off)
        {
            if (GameObject.Find("Tutorial_On") != null)
            {
                GameObject.Find("Tutorial_On").SetActive(false);
                tutorialOff = true;
            }
            

        }

        if(openMenu != -1)
        {
            //print(openMenu + " " + children[openMenu].gameObject.name);
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
                        if(checkButtons == 1)
                        {
                            ToggleMenu(1);
                        }
                        break;
                    case 3:
                        if (checkButtons == 1)
                        {
                            ToggleMenu(1);
                        }
                        break;
                    case 4:
                        if (checkButtons == 1)
                        {
                            ToggleMenu(1);
                        }
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
                resetScene();
                break;

            case 2:
                if (!tutorialOff)
                {
                    tutorial.turnOff();
                    GameObject.Find("Tutorial_On").SetActive(false);
                    tutorialOff = true;
                }
                break;
            case 3:
                grab = !grab;
                if (grab)
                {
                    left.grab = true;
                    right.grab = true;
                    GameObject.Find("Grab_On").GetComponent<Text>().text = "GRAB:\n ON";
                    timePassed = Time.time;
                }
                else
                {
                    left.grab = false;
                    right.grab = false;
                    GameObject.Find("Grab_On").GetComponent<Text>().text = "GRAB:\n OFF";
                    timePassed = Time.time;
                }
                break;
        }
    }

    void switchControlMenu(int buttonPressed)
    {
        switch (buttonPressed)
        {
            case 0:
                ToggleMenu(2);
                break;

            case 1:
                ToggleMenu(3);
                break;

            case 2:
                ToggleMenu(4);
                break;
            case 3:
                ToggleMenu(0);
                break;
        }
    }

    void resetScene()
    {
        print("change locations");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject globalControl = GameObject.FindGameObjectWithTag("GameController");
        globalControl.GetComponent<GlobalControl>().prevPlayerPosition = player.transform.position;
        globalControl.GetComponent<GlobalControl>().prevPlayerRotation = player.transform.rotation;
        globalControl.GetComponent<GlobalControl>().reset = true;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //Destroy(players[1]);

        //newPlayer.transform.position = placeholder.transform.position;
        //newPlayer.transform.rotation = placeholder.transform.rotation;
        //GameObject.FindGameObjectWithTag("Player").transform.position = playerPosition;
        //GameObject.FindGameObjectWithTag("Player").transform.rotation = playerRotation;
        //switch (buttonPressed)
        //{
        //    case 0:
        //        break;

        //    case 1:
        //        break;

        //    case 2:
        //        ToggleMenu(0);
        //        break;
        //}
    }
}
