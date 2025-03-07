using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveScript : MonoBehaviour
{

    [SerializeField] private Animator valveAnimator;
    
    
    public bool activeValve = false;
    

    [Header("Materials for light")]
    [SerializeField] private Material red;
    [SerializeField] private Material green;

    [Header("Cube for indicator")]
    [SerializeField] private GameObject Indicator;

    // Start is called before the first frame update
    void Start()
    {
        valveAnimator.SetBool("ActiveValve", activeValve);
        Indicator.GetComponent<Renderer>().material = red;
    }

    // Update is called once per frame
    void Update()
    {      
        
    }

    public void crankValve() {
        valveAnimator.SetTrigger("Crank");
        activeValve = !activeValve;

        if(activeValve) {
            Indicator.GetComponent<Renderer>().material = green;
        }
        else {
            Indicator.GetComponent<Renderer>().material = red;
        }

        valveAnimator.SetBool("ActiveValve", activeValve);
    }
}
