using System.Collections;
using TMPro;
using UnityEngine;

public class PromptsManager : MonoBehaviour
{

    public static PromptsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI promptText;
    private void Start()
    {
        promptText.gameObject.SetActive(false);

    }
    public void ShowPrompt(string message, float time = 0f)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = message;

        if (time != 0f)
        {
            delaySwitchPrompt(time);
        }
    }
    public void RemovePrompt()
    {
        promptText.text = "";
        promptText.gameObject.SetActive(false);
    }
    public IEnumerator delaySwitchPrompt(float time)
    {
        yield return new WaitForSeconds(time);
        RemovePrompt(); 
    }
}
