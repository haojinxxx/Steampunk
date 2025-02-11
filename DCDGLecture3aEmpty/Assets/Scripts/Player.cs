using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InventoryManager inventoryManager;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 3))
            {
                itemPickable item = hitInfo.collider.gameObject.GetComponent<itemPickable>();

                if (item != null)
                {
                    Debug.Log("Item getting picked up");
                    inventoryManager.ItemPicked(hitInfo.collider.gameObject);
                }
            }
        }
    }
}
