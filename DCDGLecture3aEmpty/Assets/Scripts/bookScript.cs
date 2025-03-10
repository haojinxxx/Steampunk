using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        RaycastHit hitinfo;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hitinfo, 2) && hitinfo.collider.CompareTag("Book"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(notas);
            }
        }
    }
}
