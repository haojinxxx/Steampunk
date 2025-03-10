using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Text promptText;
    [SerializeField] private Text journalText;
    [SerializeField] private ScrollRect scrollRect;
    private Transform camTransform;
    private bool isInteracted = false;
    private bool promptUsed = false;

    private List<string> hints = new List<string>();

    private void Start()
    {
        camTransform = player.transform.Find("CameraPivot").Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracted)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hitInfo, 2) && hitInfo.collider.CompareTag("Journal"))
            {
                promptUsed = true;
                interactPrompt.SetActive(true);
                promptText.text = "Press [E] to open Journal";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    openJournal();
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
        else
        {
            if (journalText.text == "")
            {
                journalText.text = string.Join("\n\n", hints);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitJournal();
            }

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            Debug.Log(scrollInput*100);
            if (scrollInput != 0f)
            {
                // Adjust the vertical normalized position of the scroll view based on the mouse wheel input
                float newScrollPosition = scrollRect.verticalNormalizedPosition + scrollInput;
                newScrollPosition = Mathf.Clamp01(newScrollPosition); // Clamp the value between 0 and 1

                scrollRect.verticalNormalizedPosition = newScrollPosition;
            }
        }
    }

    private void openJournal()
    {
        isInteracted = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        mainUI.SetActive(false);
        transform.Find("Hints").gameObject.SetActive(true);

    }

    private void exitJournal()
    {
        isInteracted = false;
        journalText.text = "";
        player.GetComponent<PlayerMovement>().enabled = true;
        mainUI.SetActive(true);
        transform.Find("Hints").gameObject.SetActive(false);
    }

    public void addHint(string hint)
    {
        if (!hints.Contains(hint))
        {
            hints.Add(hint);
            Debug.Log("Hint added");
        }
    }
}
