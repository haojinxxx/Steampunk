using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementHint : MonoBehaviour
{
    public GameObject radio;
    private bool firstTime = true;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
            firstTime = false;
            radio.GetComponent<radioScript>().playBasementClue();
        }
    }
}
