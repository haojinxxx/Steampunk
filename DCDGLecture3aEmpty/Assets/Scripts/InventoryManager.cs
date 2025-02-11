using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] hotbarSlots = new GameObject[3];
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Camera cam;

    int selectedHotbarSlot = 0;
    void Start()
    {
        HotbarItemChanged();
    }

    void Update()
    {
        CheckForHotbarInput();

    }

    private void CheckForHotbarInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 position = ray.GetPoint(3);

        ItemSO itemSO = hotbarSlots[slotIndex].GetComponent<InventorySlot>().heldItem.GetComponent<InventoryItem>().itemScriptableObject;
        Debug.Log(itemSO);
        GameObject droppedItem = Instantiate(itemSO.prefab, position, new Quaternion());
        droppedItem.GetComponent<itemPickable>().itemScriptableObject = itemSO;

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
