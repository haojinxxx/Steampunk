using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Transform camTransform;

    private Animator chestAnimation;
    private bool allowedToOpen = false;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        chestAnimation = GetComponent<Animator>();
    }
    public void allowChest()
    {
        allowedToOpen = true;
    }

    public void notAllowChest()
    {
        allowedToOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowedToOpen)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hitInfo, 2) && hitInfo.collider.CompareTag("Chest"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isOpen = !isOpen;
                    chestAnimation.SetBool("openChest", isOpen);
                }
            }
        }
    }
}
