using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    //[TextArea] public string fullText;
    public float typeSpeed = 0.05f;
    public GameObject foodImage;
    public GameObject goToKitchenButton;

    private bool skip = false;

    private void Start()
    { 
    // Hide the food image and go to kitchen button initially
    foodImage.SetActive(false);
    goToKitchenButton.SetActive(false);

    Debug.Log("Before ForceMeshUpdate - text: " + dialogueText.text);
    Debug.Log("Before ForceMeshUpdate - characterCount: " + dialogueText.textInfo.characterCount);
    
    dialogueText.ForceMeshUpdate();
    
    Debug.Log("After ForceMeshUpdate - text: " + dialogueText.text);
    Debug.Log("After ForceMeshUpdate - characterCount: " + dialogueText.textInfo.characterCount);

    StartCoroutine(ShowText()); 
    }

    // Show the text character by character
    IEnumerator ShowText()
    {
        TMP_TextInfo textInfo = dialogueText.textInfo;
        int totalVisibleCharacters = textInfo.characterCount;
        int counter = 0;

        StartCoroutine(DetectSkip());

        while (counter <= totalVisibleCharacters)
        {
            if (skip)
            {
                dialogueText.maxVisibleCharacters = totalVisibleCharacters;
                break;
            }

            dialogueText.maxVisibleCharacters = counter;
            counter++;
            
            yield return new WaitForSeconds(typeSpeed);
        }
        //Debug.Log('fullText:' + fullText);
        yield return new WaitForSeconds(0.5f);
        foodImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goToKitchenButton.SetActive(true);
    }

    IEnumerator DetectSkip()
    {
        while (!skip)
        {
            if (Input.GetMouseButtonDown(0))
                skip = true;
            yield return null;
        }
    }
}
