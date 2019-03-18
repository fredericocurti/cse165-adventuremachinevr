using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Transform user;
    float messageTime;

    int min_index = 0;
    int numChar = 1;
    int tutorial_step = 0;
    int wait = 0;

    const int LINE_MAX = 28;
    const int MAX_CHAR = 66;

    string movingMessage = "Come closer to the desk to get started.";
    string lpMessage = "Great! Now see the launchpad in front of you? Let's start making a mix. Press one of the blue buttons by ___.";

    // Start is called before the first frame update
    void Start()
    {
        messageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial_step < 2)
        {
            switch (tutorial_step)
            {
                case 0:
                    if(Time.time - messageTime >= 3.0f)
                    {
                        gameObject.GetComponent<Text>().text = movingMessage.Substring(min_index, numChar);
                        if(min_index + numChar < movingMessage.Length && wait == 5)
                        {
                            numChar++;
                            wait = 0;
                        }
                        else if(wait < 5)
                        {
                            wait++;
                        }
                        movingTutorial();
                    }
                    break;

                case 1:
                    gameObject.GetComponent<Text>().text = lpMessage.Substring(min_index, numChar);

                    if(min_index + numChar < lpMessage.Length && wait == 10)
                    {
                        numChar++;
                        wait = 0;

                        checkTextWidth(lpMessage);
                    }
                    else if (wait < 10)
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
        if(Mathf.Abs(pos.z - 2.8f) < 0.8f)
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

            gameObject.GetComponent<Text>().text = movingMessage.Substring(min_index, 1);
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
