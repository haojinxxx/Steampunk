using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    Animator animator;
    public bool inside;
    bool doorOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.name =="Player")
        {

            inside = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            inside = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed");
            doorOpen = !doorOpen;
        }
        if (doorOpen)
        {
            animator.SetBool("open", true);
        }
        else
        {
            animator.SetBool("open", false);
        }
    }
}
