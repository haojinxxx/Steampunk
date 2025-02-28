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

    private Bool animationActive;

    // Start is called before the first frame update
    void Start()
    {
        valveAnimator.SetBool("ActiveValve", activeValve);
        animationActive = false;
    }

    // Update is called once per frame
    void Update()
    {      
        if (!animationActive && Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, valveLayerMask))
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                valveAnimator.SetTrigger("Crank");
                activeValve = !activeValve;
                valveAnimator.SetBool("ActiveValve", activeValve);
            }
        }
    }
}
