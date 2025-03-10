using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class radioScript : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask radioMask;
    
    [Header("UI elements")]
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;

    [Header("List of hints")]
    [SerializeField] private hintAudio[] hints;
    private int hintIndex;

    private float reach;
    private bool promptUsed;

    [Header("Puzzles to complete")]
    [SerializeField] private ClockPuzzleManager clockPuzzle;
    [SerializeField] private paintingScript paintingScript;
    [SerializeField] private doorScript doorScript;
    [SerializeField] private Journal journalScript;
    

    private AudioSource radioAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        radioAudioSource = GetComponent<AudioSource>();
        hintIndex = 0;
        radioAudioSource.clip = hints[hintIndex].hintAudioClip;
        PlayAudio();

        reach = 2f;
        promptUsed = false;
    }

    private void NextHint() {
        hintIndex++;
        updateAudio(hintIndex);
        PlayAudio();
        Debug.Log("In NextHint");
    }

    
    /// <summary>
    /// Skips giving hints to puzzles that's already been completed
    /// </summary>
    public void TryNextHint() {
        Debug.Log(hintIndex + ": tryNextHint");     
        if(hintIndex == 0 && clockPuzzle.puzzleComplete) // Check if next hint is the clock puzzle
        {
            Debug.Log("In first if");
            hintIndex++;

            TryNextHint();
            
        }
        else if((hintIndex == 1 || hintIndex == 2) && paintingScript.puzzleComplete) {
            Debug.Log("In second if");
            if(hintIndex == 1) {hintIndex++;}
            hintIndex++;
            TryNextHint();
        }
        else if(hintIndex == 3 && !doorScript.doorUnlocked) {
            Debug.Log("In third if");
            updateAudio(5);
            PlayAudio();
        }
        else if(hintIndex == 4) { // All hints played
            Debug.Log("In fourth if");
        }
        else {NextHint();}
    }

    void updateAudio(int index) {
        Debug.Log("In updateAudio: " + index);
        radioAudioSource.clip = hints[index].hintAudioClip;
    }

    public void PlayAudio() {
        radioAudioSource.Play();
        journalScript.addHint(hints[hintIndex].hintText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, radioMask))
        {
            DisplayInputPrompt("Press [E] to Interact");
            if(Input.GetKeyDown(KeyCode.E)) {
                TryNextHint();                
            }
        }
        else {
            if (promptUsed)
            {
                interactPrompt.SetActive(false);
                promptUsed = false;
            }
        }
    }

    private void DisplayInputPrompt(string text)
    {
        promptUsed = true;
        interactPrompt.SetActive(true);
        promptText.text = text;
    }

    public void playBasementClue()
    {
        updateAudio(6);
        PlayAudio();
        journalScript.addHint(hints[6].hintText);
    }
}
