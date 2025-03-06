using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject lightObj;
    private bool lightOn = true;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<AudioSource>().Play();
            lightOn = !lightOn;

        }
        if (lightOn)
        {
            lightObj.SetActive(true);
        }
        else
        {
            lightObj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            playerInRange = false;
        }
    }
}
