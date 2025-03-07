using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Chest chestScript; // Reference to the Chest script

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chest")) // Ensure your chest has the tag "Chest"
        {
            Debug.Log("Chest has left the area!");
            chestScript.allowChest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            chestScript.notAllowChest();
        }
    }
}
