using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookScript : MonoBehaviour
{
    [SerializeField] Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hitinfo, 2) && hitinfo.collider.CompareTag("Book"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("notas");
            }
        }
    }
}
