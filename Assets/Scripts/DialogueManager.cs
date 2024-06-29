using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText;
    public float timeForEachSentence = 1f;


    private float _elapsedTime;
    private List<string> currentDialogue;
    private int currentDialogueIndex;
    private bool isDialogueOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDialogue == null) return;//If no dialogue do nothing


        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= timeForEachSentence)
        {

            if (currentDialogueIndex < currentDialogue.Count)
            {
                currentDialogueIndex++;
            }
            else
            {
                currentDialogue = null;
                currentDialogueIndex = -1;
            }

            dialogueText.text = currentDialogue[currentDialogueIndex];


            _elapsedTime = 0f;
        }
    }

    public void ShowDialogue(List<string> dialogues)
    {
        currentDialogue = dialogues; 
    }
}
