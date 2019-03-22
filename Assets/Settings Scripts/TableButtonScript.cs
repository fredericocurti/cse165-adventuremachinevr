using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableButtonScript : MonoBehaviour
{
    public string direction;
    public GameObject table;
    private bool changing = false;
    // Start is called before the first frame update
    private void Start()
    {
        table = transform.parent.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        changing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        changing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            table.transform.position += new Vector3(0f, 0.25f * (direction == "up" ? 1f : -1f), 0f) * Time.deltaTime;
        }

    }
}
