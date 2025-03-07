using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveScript : MonoBehaviour
{

    [SerializeField] private Animator valveAnimator;
    [SerializeField] private float reach;
    [SerializeField] private LayerMask valveLayerMask;
    [SerializeField] private Transform playerCameraTransform; 
    public bool activeValve = false;
    private ValveScript script;


    // Start is called before the first frame update
    void Start()
    {
        valveAnimator.SetBool("ActiveValve", activeValve);
    }

    // Update is called once per frame
    void Update()
    {      
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, valveLayerMask))
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                raycastHit.transform.TryGetComponent<ValveScript>(out script);
                script.crankValve();
            }
        }
    }

    public void crankValve() {
valveAnimator.SetTrigger("Crank");
                activeValve = !activeValve;
                valveAnimator.SetBool("ActiveValve", activeValve);
    }
}
