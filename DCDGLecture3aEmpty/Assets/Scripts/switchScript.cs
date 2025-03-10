using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchScript : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject lightObj;
    private bool playerInRange = false;
    private bool lightOn = true;

    private float reach = 2;

    private bool promptUsed;

    [Header("UI elements")]
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit raycastHit, reach) && raycastHit.collider.CompareTag("switch"))
        {
            DisplayInputPrompt("Press [E] to Interact");
            if(Input.GetKeyDown(KeyCode.E)) {
                flickSwitch();             
            }
        }
        else {
            if (promptUsed)
            {
                interactPrompt.SetActive(false);
                promptUsed = false;
            }
        }

    
    }

    private void flickSwitch() {
        gameObject.GetComponent<AudioSource>().Play();
        lightOn = !lightOn;
        lightObj.SetActive(lightOn);
    }

     private void DisplayInputPrompt(string text)
    {
        promptUsed = true;
        interactPrompt.SetActive(true);
        promptText.text = text;
    }
}
