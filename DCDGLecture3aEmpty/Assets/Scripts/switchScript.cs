using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject lightObj;
    private bool playerInRange = false;
    private bool lightOn = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitinfo;
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
