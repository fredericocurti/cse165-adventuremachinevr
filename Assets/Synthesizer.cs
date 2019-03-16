using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesizer : MonoBehaviour
{
    public Transform white_key_prefab;
    public Transform black_key_prefab;
    public Transform type_button_prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject synthesizer = GameObject.FindWithTag("Synthesizer");

        Transform newKey;
        float x = -3.57f;
        for(int i = 0; i < 29; i++)
        {
            newKey = Instantiate(white_key_prefab, synthesizer.transform);
            newKey.localPosition = new Vector3(x, 0.17f, -0.15f);
            x += 0.327f;
        }

        x = -3.4065f;
        int numKeys = 2;
        int counter = 1;
        for (int j = 0; j < 20; j++)
        {
            newKey = Instantiate(black_key_prefab, synthesizer.transform);
            newKey.localPosition = new Vector3(x, 0.27f, 0.05f);

            if(counter < numKeys)
            {
                x += 0.327f;
                counter++;
            }
            else if(numKeys == 2)
            {
                numKeys = 3;
                counter = 1;
                x += 0.654f;
            }
            else if(numKeys == 3)
            {
                numKeys = 2;
                counter = 1;
                x += 0.654f;
            }
        }

        x = -6.0f;
        for (int k = 0; k < 4; k++)
        {
            newKey = Instantiate(type_button_prefab, synthesizer.transform);
            newKey.localPosition = new Vector3(x, 0.445f, 0.71f);

            x += 0.6f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
