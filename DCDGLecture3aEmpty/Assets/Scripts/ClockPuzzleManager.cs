using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzleManager : MonoBehaviour
{

    [SerializeField] Transform minuteHand, hourHand;
    [SerializeField] GameObject clockGear;
    [SerializeField] Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.L))
        {
            clockGear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<ClockPuzzleManager>().enabled = false;
            
        }
    }
}
    