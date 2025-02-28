//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class ObjectGrabbable : MonoBehaviour
//{
//
//    private Rigidbody objectRigidbody;
//    private Transform objectGrabPointTransform;
//
//    private void Awake()
//    {
//        objectRigidbody = GetComponent<Rigidbody>();
//    }
//
//    public void Grab(Transform objectGrabPointTransform)
//    {
//        this.objectGrabPointTransform = objectGrabPointTransform;
//        objectRigidbody.useGravity = false;
//        objectRigidbody.isKinematic = true;
//    }
//
//    public void Drop()
//    {
//        this.objectGrabPointTransform = null;
//        objectRigidbody.useGravity = true;
//        objectRigidbody.isKinematic = false;
//    }
//
//    private void FixedUpdate()
//    {
//        if (objectGrabPointTransform != null)
//        {
//            float lerpSpeed = 10f;
//            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
//            objectRigidbody.MovePosition(newPosition);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private float moveSpeed = 40f;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.drag = 10f; // Helps stabilize object
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.drag = 0f; // Reset drag
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 direction = (objectGrabPointTransform.position - transform.position);
            objectRigidbody.velocity = direction * moveSpeed;
        }
    }
}
