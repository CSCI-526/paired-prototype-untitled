using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; 
    public float typeSpeed = 0.05f;
    public GameObject foodImage;
    public GameObject goToKitchenButton;

    private bool skip = false;

    private void Start()
    { 
    // Hide the food image and go to kitchen button initially
    foodImage.SetActive(false);
    goToKitchenButton.SetActive(false);
 
    dialogueText.ForceMeshUpdate();
      
    StartCoroutine(ShowText()); 
    }

    // Show the text character by character
    IEnumerator ShowText()
    {
        TMP_TextInfo textInfo = dialogueText.textInfo;
        int totalVisibleCharacters = textInfo.characterCount;
        Debug.Log("totalVisibleCharacters: " + totalVisibleCharacters);
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
        yield return new WaitForSeconds(0.5f);
        foodImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        goToKitchenButton.SetActive(true);
    }

    // Detect if the user clicks the mouse button to skip the text
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
