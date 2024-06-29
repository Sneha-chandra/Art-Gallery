using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Talker : MonoBehaviour
{
    public List<string> dialogueText;
    public List<AudioClip> dialogueAudio;
    public TextMeshProUGUI dialogueTextObj;
    public AudioSource audioSource;

    private bool inRange = false;
    private bool isSpeaking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    int dialogueIndexCurrent = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && inRange && !isSpeaking)
        {

            dialogueIndexCurrent = 0;

            dialogueTextObj.text = dialogueText[dialogueIndexCurrent];
            audioSource.clip = dialogueAudio[dialogueIndexCurrent];
            audioSource.Play();

            isSpeaking = true;
        }


        if(isSpeaking)
        {
            if(!audioSource.isPlaying)
            {
                if(dialogueIndexCurrent < dialogueAudio.Count - 1)
                {
                    dialogueIndexCurrent++;
                    dialogueTextObj.text = dialogueText[dialogueIndexCurrent];
                    audioSource.clip = dialogueAudio[dialogueIndexCurrent];
                    audioSource.Play();
                }
                else
                {
                    StopSpeaking();
                    isSpeaking =false;
                }
            }
        }
    }
    public void StopSpeaking()
    {
        dialogueTextObj.text = "";
        dialogueIndexCurrent = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            inRange = true;
            PromptsManager.Instance.ShowPrompt("Press Y To Talk", 5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            inRange = false;

            PromptsManager.Instance.RemovePrompt();
        }
    }
}