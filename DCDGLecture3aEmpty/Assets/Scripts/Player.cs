using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;

    void Start()
    {
    }

    void Update()
    {
    }
}
