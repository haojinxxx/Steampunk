using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorScript : MonoBehaviour
{
    Animator animator;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Transform camTransform;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    private bool inside;
    private bool doorOpen = false;
    private bool gearPlaced = false;
    public bool doorUnlocked = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.name =="Player")
        {

            inside = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            inside = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (inside)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hitinfo, 2) && hitinfo.collider.CompareTag("Door"))
            {
                if (!doorUnlocked)
                {
                    InventorySlot slot = inventoryManager.getCurrentSlot();
                    if (slot.heldItem != null && slot.heldItem.GetComponent<InventoryItem>().itemScriptableObject.prefab.tag == "Key")
                    {
                        if (gearPlaced && Input.GetKeyDown(KeyCode.E))
                        {
                            GetComponent<AudioSource>().Play();
                            doorUnlocked = true;
                        }
                        else if (!gearPlaced && Input.GetKeyDown(KeyCode.E))
                        {
                            interactPrompt.SetActive(true);
                            promptText.text = "The unlocking mechanism seems to be missing a gear";

                            StartCoroutine(DelayedPromptRemove(3f));
                        }
                    }
                    else if (slot.heldItem != null && slot.heldItem.GetComponent<InventoryItem>().itemScriptableObject.prefab.tag == "DoorGear")
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(slot.heldItem);
                            interactPrompt.SetActive(true);
                            promptText.text = "Gear has been placed";

                            StartCoroutine(DelayedPromptRemove(2f));
                            gearPlaced = true;
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        doorOpen = !doorOpen;
                        animator.SetBool("open", doorOpen);
                    }
                }

            }
        }
    }

    IEnumerator DelayedPromptRemove(float delay)
    {
        yield return new WaitForSeconds(delay);
        disableHintPrompt();
    }

    private void disableHintPrompt()
    {
        interactPrompt.SetActive(false);
    }
}
