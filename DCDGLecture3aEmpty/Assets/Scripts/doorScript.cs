using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    Animator animator;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Transform camTransform;
    private bool inside;
    private bool doorOpen = false;
    private bool gearPlaced = false;
    private bool doorUnlocked = false;


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
                            doorUnlocked = true;
                        }
                        else if (!gearPlaced && Input.GetKeyDown(KeyCode.E))
                        {
                            Debug.Log("Gear has not been placed");
                        }
                    }
                    else if (slot.heldItem != null && slot.heldItem.GetComponent<InventoryItem>().itemScriptableObject.prefab.tag == "DoorGear")
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Destroy(slot.heldItem);
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
}
