using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockPuzzleManager : MonoBehaviour
{

    [SerializeField] Transform minuteHand, hourHand;
    [SerializeField] GameObject clockGear;
    [SerializeField] ItemSO gearSO;
    [SerializeField] Camera cam;
    [SerializeField] GameObject puzzleCamPosition;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    private int rotationAngle;
    public float transitionSpeed = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool promptUsed;

    // Start is called before the first frame update
    void Start()
    {
        rotationAngle = 120;
        originalPosition = cam.transform.position;
        originalRotation = cam.transform.rotation;
        promptUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitinfo, 3, LayerMask.GetMask("ClockPuzzle")))
        {
            promptUsed = true;
            interactPrompt.SetActive(true);
            promptText.text = "Press [E] to interact";

            if (Input.GetKeyDown(KeyCode.E))
            {
                rotationAngle += 30;
                hourHand.rotation = Quaternion.Euler(rotationAngle, 0, 0);
                if (rotationAngle % 360 == 90)
                {

                    clockGear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    clockGear.AddComponent<itemPickable>();
                    clockGear.GetComponent<itemPickable>().itemScriptableObject = gearSO;
                    clockGear.GetComponent<Rigidbody>().AddForce(clockGear.transform.forward * 150f);
                    interactPrompt.SetActive(false);
                    this.GetComponent<ClockPuzzleManager>().enabled = false;
                }
            }
        }
        else
        {
            if (promptUsed)
            {
                promptUsed = false;
                interactPrompt.SetActive(false);
            }
        }
    }
}
    