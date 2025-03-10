using System;
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
    [SerializeField] Camera puzzleCam;
    [SerializeField] GameObject player;
    [SerializeField] GameObject PuzzleUI, MainUI;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    private int hourRotationAngle;
    private int minuteRotationAngle;
    public float transitionSpeed = 2f;

    private int clockHourSectors;
    private int clockMinuteSectors;

    private bool promptUsed;

    private bool isInteracting = false;
    private bool puzzleActive = false;
    public bool puzzleComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        puzzleCam.enabled = false;
        puzzleCam.gameObject.SetActive(false);
        hourRotationAngle = -60;
        minuteRotationAngle = 0;
        clockHourSectors = 2; //start time
        clockMinuteSectors = 0; //start time   
        promptUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting && Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitinfo, 2, LayerMask.GetMask("ClockPuzzle")))
        {
            promptUsed = true;
            interactPrompt.SetActive(true);
            promptText.text = "Press [E] to interact";

            if (Input.GetKeyDown(KeyCode.E))
            {
                enterPuzzleMode();
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

        if (puzzleActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                minuteRotationAngle -= 30;
                clockMinuteSectors += 1;
                minuteHand.rotation = Quaternion.Euler(minuteRotationAngle, 0, 0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                hourRotationAngle -= 30;
                clockHourSectors += 1;
                hourHand.rotation = Quaternion.Euler(hourRotationAngle, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitPuzzleMode();
            }
            
            if (clockMinuteSectors % 12 == 11 && clockHourSectors % 12 == 8)
            {

                clockGear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                clockGear.AddComponent<itemPickable>();
                clockGear.GetComponent<itemPickable>().itemScriptableObject = gearSO;
                clockGear.GetComponent<Rigidbody>().AddForce(clockGear.transform.forward * 150f);
                interactPrompt.SetActive(false);

                StartCoroutine(DelayedExit(1f));

                puzzleComplete = true;
                this.GetComponent<ClockPuzzleManager>().enabled = false;
            }
        }
    }

    private void enterPuzzleMode()
    {
        isInteracting = true;

        player.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;

        puzzleCam.enabled = true;
        puzzleCam.gameObject.SetActive(true);

        puzzleActive = true;

        PuzzleUI.SetActive(true);
        MainUI.SetActive(false);

    }
    private void exitPuzzleMode()
    {
        isInteracting = false;
        puzzleActive = false;

        puzzleCam.enabled = false;
        puzzleCam.gameObject.SetActive(false);

        player.GetComponent<PlayerMovement>().enabled = true;
        player.SetActive(true);

        PuzzleUI.SetActive(false);
        MainUI.SetActive(true);
    }

    IEnumerator DelayedExit(float delay)
    {
        yield return new WaitForSeconds(delay);
        exitPuzzleMode();
    }
}
    