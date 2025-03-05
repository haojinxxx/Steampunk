using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class paintingScript : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pushableMask;
    private float reach = 2f;
    [SerializeField] private Animator paintingAnimator;

    [SerializeField] private Animator SandClockAnimator1;
    [SerializeField] private Animator SandClockAnimator2;
    
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;

    private bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, pushableMask) && interactable)
        {
            DisplayInputPrompt("Press [E] to Interact");
            if(Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("hehe");
                paintingAnimator.SetTrigger("PressPainting");

                SandClockAnimator1.SetTrigger("StopAnimation");
                SandClockAnimator2.SetTrigger("StopAnimation");

                interactable = false;
            }
        }
        else {
            interactPrompt.SetActive(false);
        }
    }

    private void DisplayInputPrompt(string text)
    {
        interactPrompt.SetActive(true);
        promptText.text = text;
    }
}
