using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speed;
    public float angle;
    public Vector3 direction;
    public bool allowOpen;
    // Start is called before the first frame update
    void Start()
    {
        angle=transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Round(transform.eulerAngles.y)!= angle)
        {
            transform.Rotate(direction * speed);
        }
        if (Input.GetMouseButtonDown(0) && allowOpen == true)
            
        {
            angle = 80;
            direction = Vector3.up;
        }
    }

    void OnTriggerStray(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            allowOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            allowOpen = false;
        }
    }
}
