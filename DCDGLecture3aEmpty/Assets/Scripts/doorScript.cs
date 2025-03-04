using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    Animator animator;
    public bool playerInside = false;
    bool door = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();


    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            door = !door;
        }
        if (door)
        {
            animator.SetBool("open", true);
        }
        else
        {
            animator.SetBool("open", false);
        }
    }
}
