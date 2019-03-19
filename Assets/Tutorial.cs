using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Transform user;
    float messageTime;
    Vector3 initPos;

    int min_index = 0;
    int numChar = 1;
    int tutorial_step = 0;
    int wait = 0;

    const int LINE_MAX = 28;
    const int MAX_CHAR = 66;

    string movingMessage1 = "Come closer to the desk to get started. You can teleport by pointing at the floor with your left hand and pressing the left index finger's trigger to move to the desired spot.";
    string movingMessage2 = "Nice! You can also hold onto the trigger at your right index finger and pull yourself over like you're holding onto a rope. Try it out.";
    string lpMessage = "Now see the launchpad in front of you? Let's start making a mix. Press one of the blue buttons by ___.";

    // Start is called before the first frame update
    void Start()
    {
        messageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial_step < 3)
        {
            switch (tutorial_step)
            {
                case 0:
                    if(Time.time - messageTime >= 3.0f)
                    {
                        gameObject.GetComponent<Text>().text = movingMessage1.Substring(min_index, numChar);
                        if(min_index + numChar < movingMessage1.Length && wait == 1)
                        {
                            numChar++;
                            wait = 0;

                            checkTextWidth(movingMessage1);
                        }
                        else if(wait < 1)
                        {
                            wait++;
                        }
                        movingTutorial();
                    }
                    break;

                case 1:
                    gameObject.GetComponent<Text>().text = movingMessage2.Substring(min_index, numChar);

                    if(min_index + numChar < movingMessage2.Length && wait == 1)
                    {
                        numChar++;
                        wait = 0;

                        checkTextWidth(movingMessage2);
                    }
                    else if (wait < 1)
                    {
                        wait++;
                    }
                    draggingTutorial();
                    break;

                case 2:
                    gameObject.GetComponent<Text>().text = lpMessage.Substring(min_index, numChar);

                    if (min_index + numChar < lpMessage.Length && wait == 1)
                    {
                        numChar++;
                        wait = 0;

                        checkTextWidth(lpMessage);
                    }
                    else if (wait < 1)
                    {
                        wait++;
                    }
                    break;
            }
        }
    }

    void movingTutorial()
    {
        Vector3 pos = user.position;
        if(Mathf.Abs(pos.z - 8.0f) < 0.8f)
        {
            tutorial_step++;
            min_index = 0;
            numChar = 1;
            wait = 0;
            initPos = user.position;
        }
    }

    void draggingTutorial()
    {
        Vector3 pos = user.position;
        if (Mathf.Abs(Vector3.Distance(pos, initPos)) > 0.5f)
        {
            tutorial_step++;
            min_index = 0;
            numChar = 1;
            wait = 0;
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
