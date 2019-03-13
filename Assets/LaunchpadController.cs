using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform activeBassButton;
    public Transform activeDrumButton;
    public Queue<Transform> activeSoundButtons;

    public Image timerImage;
    private AudioController ac;

    private AudioClip bassLoop;
    private AudioClip drumLoop;
    private List<AudioClip> soundLoops;

    public int soundLoopsIndex = 0;
    int queueSize = 3;

    public float syncTimer = 0f;
    public float timerFill = 0f;
    private int soundButtonCounter = 0;

    private bool samplesReady = false;

    public void RemoveFromQueue(Queue<Transform> q, Transform t)
    {
        Transform[] temp = q.ToArray();
        q.Clear();
        q.TrimExcess();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] != t)
            {
                q.Enqueue(temp[i]);

            }
        }
    }

    private void Awake()
    {
        activeBassButton = null;
        activeDrumButton = null;
        activeSoundButtons = new Queue<Transform>(queueSize);
        createLaunchpadButtons();
    }

    void Start()
    {
        ac = GameObject.Find("AudioPlayer").GetComponent<AudioController>();
        timerImage = GameObject.Find("LaunchpadTimer").GetComponent<Image>();
        soundLoops = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPress(Transform button)
    {
        LaunchpadButtonScript lbs = button.GetComponent<LaunchpadButtonScript>();
        AudioClip buttonLoop = lbs.loop;
        Renderer buttonRenderer = button.GetComponent<Renderer>();
        string loopType = lbs.loopType;
        samplesReady = false;

        if (loopType == "bass")
        {
            if (button == activeBassButton)
            {
                activeBassButton.GetComponent<Renderer>().material.color = buttonRedColor;
                ac.ClearChannel(5);
                activeBassButton = null;
                return;
            }
            if (activeBassButton != null)
            {
                activeBassButton.GetComponent<Renderer>().material.color = buttonRedColor;
            }
            activeBassButton = button;
            buttonRenderer.material.color = buttonRedColorActive;

        } else if (loopType == "drum")
        {
            if (button == activeDrumButton)
            {
                activeDrumButton.GetComponent<Renderer>().material.color = buttonBlueColor;
                ac.ClearChannel(4);
                activeDrumButton = null;
                return;
            }
            if (activeDrumButton != null)
            {
                activeDrumButton.GetComponent<Renderer>().material.color = buttonBlueColor;
            }
            activeDrumButton = button;
            buttonRenderer.material.color = buttonBlueColorActive;
        } else
        {
            if (activeSoundButtons.Contains(button))
            {
                RemoveFromQueue(activeSoundButtons, button);
                button.GetComponent<Renderer>().material.color = buttonGreenColor;
                return;
            }

            if (activeSoundButtons.Count == 3)
            {
                activeSoundButtons.Dequeue().GetComponent<Renderer>().material.color = buttonGreenColor;
            }

            activeSoundButtons.Enqueue(button);
            button.GetComponent<Renderer>().material.color = buttonGreenColorActive;

        }
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

    void FixedUpdate()
    {
        syncTimer += Time.fixedDeltaTime;
        timerFill = syncTimer / 4.35f;
        timerImage.fillAmount = timerFill;

        if (!samplesReady)
        {
            bassLoop = activeBassButton != null ? activeBassButton.GetComponent<LaunchpadButtonScript>().loop : null;
            drumLoop = activeDrumButton != null ? activeDrumButton.GetComponent<LaunchpadButtonScript>().loop : null;
            soundLoops.Clear();
            foreach(Transform sb in activeSoundButtons.ToArray())
            {
                soundLoops.Add(sb.GetComponent<LaunchpadButtonScript>().loop);
            }
            samplesReady = true;
        }


        if (syncTimer >= 4.35f)
        {
            if (activeBassButton)
            {
                ac.PlayLoop(bassLoop, "bass");
            }
            if (activeDrumButton)
            {
                ac.PlayLoop(drumLoop, "drum");
            }
            for (int i = 0; i < soundLoops.Count; i++)
            {
                ac.PlayClipOnChannel(soundLoops[i], i + 1);
            }

            syncTimer = 0f;
            samplesReady = false;
        }
    }
}
