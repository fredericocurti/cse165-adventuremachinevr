using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadController : MonoBehaviour
{
    public Transform button;
    public List<Transform> buttons;
    public Color buttonRedColorActive;
    public Color buttonGreenColorActive;
    public Color buttonBlueColorActive;
    public Color buttonRedColor;
    public Color buttonGreenColor;
    public Color buttonBlueColor;

    public List<AudioClip> bassLoops;
    public List<AudioClip> drumLoops;
    public List<AudioClip> soundLoops;

    private AudioController ac;

    private void Awake()
    {
        bassLoops = new List<AudioClip>();
        drumLoops = new List<AudioClip>();
        soundLoops = new List<AudioClip>();
        createLaunchpadButtons();
    }

    void Start()
    {
        ac = GameObject.Find("AudioPlayer").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPress(Transform button, AudioClip audioClip)
    {
    }

    void createLaunchpadButtons()
    {
        buttons = new List<Transform>();
        Vector3 basePosition = button.position;
        float buttonSpacingFactor = 15;

        // Generate Buttons
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Vector3 buttonPosition = new Vector3(basePosition.x + (float)i / buttonSpacingFactor, button.position.y, basePosition.z + (float)j / buttonSpacingFactor);
                Transform createdButton = Instantiate(button, buttonPosition, transform.rotation, transform);
                LaunchpadButtonScript createdButtonScript = createdButton.GetComponent<LaunchpadButtonScript>();
                createdButton.name = string.Format("Button{0}{1}", i, j);

                if (i - j > 1)
                {
                    // bass
                    //createdButton.gameObject
                    createdButton.GetComponent<Renderer>().material.color = buttonRedColor;
                    createdButton.GetComponent<LaunchpadButtonScript>().loop = Resources.Load<AudioClip>(string.Format("samples/bass{0}{1}", i, j));
                    createdButtonScript.loopType = "bass";

                }
                else if (i == j || i - j == 1 || j - i == 1)
                {
                    // sound
                    createdButton.GetComponent<Renderer>().material.color = buttonGreenColor;
                    createdButton.GetComponent<LaunchpadButtonScript>().loop = Resources.Load<AudioClip>(string.Format("samples/sounds{0}{1}", i, j));
                    createdButtonScript.loopType = "sound";
                }
                else
                {
                    // drum
                    createdButton.GetComponent<Renderer>().material.color = buttonBlueColor;
                    createdButton.GetComponent<LaunchpadButtonScript>().loop = Resources.Load<AudioClip>(string.Format("samples/drum{0}{1}", i, j));
                    createdButtonScript.loopType = "drum";

                }
                buttons.Add(createdButton);
            }
        }
        button.gameObject.SetActive(false);
        transform.Rotate(new Vector3(0f, -  45f, 0f));
    }
}
