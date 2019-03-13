using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float spawnHandsTimer = 5f;
    public bool handsSpawned = false;
    bool handsHaveCollider = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (spawnHandsTimer > 0f && !handsSpawned)
        {
            spawnHandsTimer -= Time.deltaTime;
         
        } else
        {
            handsSpawned = true;
        }

        if (handsSpawned && !handsHaveCollider)
        {
            print("TIMER");
            GameObject leftIndex = GameObject.Find("hands:b_l_index_ignore");
            BoxCollider bc = leftIndex.AddComponent<BoxCollider>();
            bc.size = new Vector3(0.02f, 0.02f, 0.02f);
            bc.center = new Vector3(0.01f, 0f, 0f);
            handsHaveCollider = true;
        }
        
    }
}
