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

    public void NextHint() {
        hintIndex++;
        updateAdio(hintIndex);
        PlayAudio();
    }

    void updateAdio(int index) {
        radioAudioSource.clip = hints[hintIndex].hintAudioClip;
    }

    public void PlayAudio() {
        radioAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, radioMask))
        {
            DisplayInputPrompt("Press [E] to Interact");
            if(Input.GetKeyDown(KeyCode.E)) {
                NextHint();                
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
}
