using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Object", menuName = "Scriptable Objects/Interactable Object")]
public class InteractableSO : ScriptableObject
{
    public string name;
    public GameObject prefab;
}
