using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject[] hotbarSlots = new GameObject[3];
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    [SerializeField] private float throwForce;
    [SerializeField] Camera cam;
    [SerializeField] private Light flashlight;

    private bool promptUsed;

    int selectedHotbarSlot = 0;
    void Start()
    {
        HotbarItemChanged();
        interactPrompt.SetActive(false);
        promptUsed = false;
        flashlight.enabled = false;
    }

    void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(camTransform.position, camTransform.forward, out hitInfo, 2) && hitInfo.collider.gameObject.GetComponent<itemPickable>() != null)
        {
            itemPickable item = hitInfo.collider.gameObject.GetComponent<itemPickable>();
            DisplayInputPrompt("Press [E] to Pick Up");
            if (Input.GetKeyDown(KeyCode.E))
            {
                ItemPicked(hitInfo.collider.gameObject);
            }
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 2, LayerMask.GetMask("Placable")))
        {
            CheckForPlaceInput(hitInfo);
        }
        else
        {
            if (promptUsed)
            {
                promptUsed = false;
                interactPrompt.SetActive(false);
            }
        }

        GameObject slotItem = hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem;
        if (slotItem != null && slotItem.GetComponent<InventoryItem>().itemScriptableObject.prefab.layer == 11)
        {
            flashlight.enabled = true;
        }
        else
        {
            flashlight.enabled = false;
        }

        CheckForHotbarInput();

    }

    public InventorySlot getCurrentSlot()
    {
        return hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>();
    }

    private void CheckForPlaceInput(RaycastHit hitInfo)
    {
        if (hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem != null)
        {
            DisplayInputPrompt("Press [E] to Place Item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlaceItem(hitInfo, hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem.GetComponent<InventoryItem>().itemScriptableObject);
            }
        }
        else
        {
            if (promptUsed)
            {
                promptUsed = false;
                interactPrompt.SetActive(false);
            }
        }
    }

    private void PlaceItem(RaycastHit hitInfo, ItemSO itemSO)
    {
        GameObject obj = Instantiate(itemSO.prefab, hitInfo.point, Quaternion.identity);
        obj.transform.LookAt(hitInfo.point + hitInfo.normal);
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        obj.GetComponent<Rigidbody>().isKinematic = false;

        Destroy(hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem);
    }

    private void DisplayInputPrompt(string text)
    {
        promptUsed = true;
        interactPrompt.SetActive(true);
        promptText.text = text;
    }

    private void CheckForHotbarInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedHotbarSlot < 2) {
                selectedHotbarSlot += 1;            
            }
            HotbarItemChanged();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedHotbarSlot > 0)
            {
                selectedHotbarSlot -= 1;
            }
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedHotbarSlot = 0;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedHotbarSlot = 1;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedHotbarSlot = 2;
            HotbarItemChanged();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hotbarSlots[selectedHotbarSlot].GetComponent<InventorySlot>().heldItem)
            {
                DropHotbarItem(selectedHotbarSlot);
            }
        }
    }

    private void DropHotbarItem(int slotIndex)
    {
        ItemSO itemSO = hotbarSlots[slotIndex].GetComponent<InventorySlot>().heldItem.GetComponent<InventoryItem>().itemScriptableObject;
        GameObject droppedItem = Instantiate(itemSO.prefab, cam.transform.Find("DropPoint").position, new Quaternion());
        droppedItem.GetComponent<itemPickable>().itemScriptableObject = itemSO;

        Vector3 throwDirection = cam.transform.forward + new Vector3(0,0.2f, 0);

        droppedItem.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);

        Destroy(hotbarSlots[slotIndex].GetComponent<InventorySlot>().heldItem);
    }

    private void HotbarItemChanged()
    {

        foreach (GameObject slot in hotbarSlots)
        {
            Vector3 scale;

            if (slot == hotbarSlots[selectedHotbarSlot])
            {
                scale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                scale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            slot.transform.localScale = scale;
        }
    }

    public void ItemPicked(GameObject pickedItem)
    {
        Debug.Log(pickedItem);
        GameObject emptySlot = null;

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            InventorySlot slot = hotbarSlots[i].GetComponent<InventorySlot>();

            if (slot.heldItem == null)
            {
                emptySlot = hotbarSlots[i];
                break;
            }
        }

        if (emptySlot != null)
        {
            GameObject newItem = Instantiate(itemPrefab);
            newItem.GetComponent<InventoryItem>().itemScriptableObject = pickedItem.GetComponent<itemPickable>().itemScriptableObject;
            newItem.transform.SetParent(emptySlot.transform.parent.parent.GetChild(2));
            newItem.GetComponent<InventoryItem>().stackCurrent = 1;

            emptySlot.GetComponent<InventorySlot>().SetHeldItem(newItem);
            newItem.transform.localScale = new Vector3(1, 1, 1);

            Destroy(pickedItem);
        }
    }
}
