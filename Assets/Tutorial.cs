using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Transform user;
    OVRGrabber left;
    OVRGrabber right;
    MainMenuScript settings;

    float messageTime;
    Vector3 initPos;
    bool wasPressed;

    int min_index = 0;
    int numChar = 1;
    int tutorial_step = 0;
    int wait = 0;

    string[] messages = { "Welcome to Machine Adventure VR!",
                          "Come closer to the desk to get started. You can teleport by pointing at the floor with your left hand and pressing the left index finger's trigger to move to the desired spot.",
                          "Nice! You can also hold onto the trigger at your right index finger and pull yourself over like you're pulling a rope. Try it out.",
                          "Now try to grab an object in the room by pressing the middle finger trigger on either hand.",
                          "Alright, you can put that down now.",
                          "To use the launchpad or the synthesizer, just press the buttons and keys with your virtual finger like you would in the real world. " + 
                          "You can experiment with that on your own.",
                          "Finally, the settings. Try opening and closing the settings menu by pressing the button on your wrist.",
                          "Nice. You can change locations and check the controls in the settings menu.",
                          "Congratulations, you now know all of the controls! Have fun making music. Bye :)" };

    // Start is called before the first frame update
    void Start()
    {
        left = GameObject.Find("AvatarGrabberLeft").GetComponent<OVRGrabber>();
        right = GameObject.Find("AvatarGrabberRight").GetComponent<OVRGrabber>();
        settings = GameObject.Find("Menu").GetComponent<MainMenuScript>();
        messageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial_step < 9)
        {
            printMessage();

            Vector3 pos = user.position;
            switch (tutorial_step)
            {
                case 0:
                    if(min_index + numChar == messages[tutorial_step].Length && Time.time - messageTime > 4.0f)
                    {
                        resetValues();
                    }
                    break;

                case 1:
                    if (Mathf.Abs(pos.z - GameObject.Find("Table").transform.position.z) < 2.0f)
                    {
                        resetValues();
                    }
                    initPos = user.position;
                    break;

                case 2:
                    if (Mathf.Abs(Vector3.Distance(pos, initPos)) > 0.3f)
                    {
                        resetValues();
                        Debug.Log(tutorial_step);
                    }
                    break;

                case 3:
                    if (left.grabbedObject != null || right.grabbedObject != null)
                    {
                        resetValues();
                    }
                    break;
                case 4:
                    if (left.grabbedObject == null && right.grabbedObject == null)
                    {
                        resetValues();
                        messageTime = Time.time;
                    }
                    break;
                case 5:
                    if (min_index + numChar == messages[tutorial_step].Length && Time.time - messageTime > 18.0f)
                    {
                        resetValues();
                    }
                    break;
                case 6:
                    if (settings.menuActive && !wasPressed)
                    {
                        wasPressed = true;
                    }
                    else if(!settings.menuActive && wasPressed)
                    {
                        resetValues();
                        messageTime = Time.time;
                    }
                    break;
                case 7:
                    if (min_index + numChar == messages[tutorial_step].Length && Time.time - messageTime > 8.0f)
                    {
                        resetValues();
                        messageTime = Time.time;
                    }
                    break;
                case 8:
                    Debug.Log(Time.time - messageTime);

                    if (min_index + numChar == messages[tutorial_step].Length && Time.time - messageTime > 7.5f)
                    {
                        gameObject.transform.parent.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    void resetValues()
    {
        tutorial_step++;
        min_index = 0;
        numChar = 1;
        wait = 0;
    }

    void printMessage()
    {
        gameObject.GetComponent<Text>().text = messages[tutorial_step].Substring(min_index, numChar);

        if (min_index + numChar < messages[tutorial_step].Length && wait == 3)
        {
            numChar++;
            wait = 0;

            checkTextWidth(messages[tutorial_step]);
        }
        else if (wait < 3)
        {
            wait++;
        }
    }

    //Return whether the text is too wide for the current field.  
    void checkTextWidth(string displayMessage)
    {
        gameObject.GetComponent<Text>().text = displayMessage.Substring(min_index, numChar);

        float textHeight = LayoutUtility.GetPreferredHeight(gameObject.GetComponent<Text>().rectTransform); //This is the width the text would LIKE to be
        float contHeight = gameObject.transform.GetComponent<RectTransform>().rect.height; //This is the actual width of the text's parent container

        if(textHeight > contHeight)
        {
            int prevCount = 1;
            int count = 1;

            gameObject.GetComponent<Text>().text = displayMessage.Substring(min_index, 1);
            float textWidth = LayoutUtility.GetPreferredWidth(gameObject.GetComponent<Text>().rectTransform); //This is the width the text would LIKE to be
            float contWidth = gameObject.transform.GetComponent<RectTransform>().rect.width; //This is the actual width of the text's parent container

            while (textWidth < contWidth)
            {
                prevCount = count;
                count++;

                while (displayMessage[min_index + count - 1] != ' ' && min_index + count < displayMessage.Length)
                {
                    count++;
                }

                gameObject.GetComponent<Text>().text = displayMessage.Substring(min_index, count);
                textWidth = LayoutUtility.GetPreferredWidth(gameObject.GetComponent<Text>().rectTransform); //This is the width the text would LIKE to be
            }

            gameObject.GetComponent<Text>().text = displayMessage.Substring(min_index, numChar);
            min_index = min_index + prevCount - 1;
            numChar = numChar - prevCount;
        }
        else
        {
            gameObject.GetComponent<Text>().text = displayMessage.Substring(min_index, numChar - 1);
        }
    }
}
