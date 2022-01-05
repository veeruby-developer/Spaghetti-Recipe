using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneUIManager : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("UI panels")]
    [SerializeField]
    public GameObject mainPage;
    [SerializeField]
    public GameObject modePage;
    [SerializeField]
    public GameObject instructionPage;
    [SerializeField]
    public GameObject togglePage;
    [SerializeField]
    public GameObject homeTogglePage;
    [SerializeField]
    public GameObject eyeMessagePanel;
    [SerializeField]
    public GameObject recipeInstructionPage;

    [Tooltip("UI text")]
    [SerializeField]public TextMeshProUGUI voiceMessage;
    [SerializeField]public TextMeshProUGUI eyeMessage;
    [SerializeField]public TextMeshProUGUI instructionSteps;

    [Header("Object")]
    [Tooltip("Anchor Cube")]
    [SerializeField] public GameObject anchorCube;

    [Header("Instructions")]
    [Tooltip("Recipe steps")]
    [SerializeField] public string[] insruction;

    [Header("Audio")]
    [Tooltip("Audio source")]
    [SerializeField] public AudioSource audio;

    [Header("ToolTip Objects")]
    [SerializeField] public GameObject stove;
    [SerializeField] public GameObject fridge;
    [SerializeField] public GameObject sauce;
    [SerializeField] public GameObject noodles;
    [SerializeField] public GameObject sink;

    int count;

    #region Public Methods

    //Function for welcome panel button
    public void LoadMainMenu()
    {
        mainPage.SetActive(false);
        modePage.SetActive(true);
    }
    public void HomeFunction()
    {
        anchorCube.SetActive(false);
        togglePage.SetActive(false);
        homeTogglePage.SetActive(false);
        recipeInstructionPage.SetActive(false);
        modePage.SetActive(true);
    }

    //Setup mode button function with popup messages
    public void StartSetupMode()
    {
        modePage.SetActive(false);
        instructionPage.SetActive(true);
    }

    public void InstructionPageFunction()
    {
        instructionPage.SetActive(false);
        anchorCube.SetActive(true);
        togglePage.SetActive(true);
    }

    //Recipe mode button function with popup messages
    public void StartRecipeMode()
    {
        modePage.SetActive(false);
        anchorCube.SetActive(true);
        recipeInstructionPage.SetActive(true);

        Transform[] allChildren = anchorCube.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.tag == "Child")
            {
                child.gameObject.SetActive(false);
                //child.GetComponent<>
            }
            

        }
    }
    

    //Function for eyegaze on setup mode
    public void EnableEyeTrackingSetupMode()
    {
        eyeMessage.text = "This mode helps you to setup the cooking experince";
        eyeMessagePanel.SetActive(true);
    }
    //Function for eyegaze on recipe mode
    public void EnableEyeTrackingRecipeMode()
    {
        eyeMessage.text = "This mode helps you to cook the recipe";
        eyeMessagePanel.SetActive(true);
    }
    public void DisableEyeTracking()
    {
        eyeMessagePanel.SetActive(false);
    }

    public void NextFunction()
    {
        
        if (count < insruction.Length)
        {
            Debug.Log("Count = " + count);
            if (count == 1)
            {
                instructionSteps.text = insruction[count];
                //(AudioDelay());
                anchorCube.SetActive(true);
                stove.SetActive(true);
                fridge.SetActive(false);
                sauce.SetActive(false);
                noodles.SetActive(false);
                sink.SetActive(false);

            }
            if (count == 2)
            {
                instructionSteps.text = insruction[count];
                //StartCoroutine(AudioDelay());
                anchorCube.SetActive(true);
                stove.SetActive(false);
                fridge.SetActive(true);
                sauce.SetActive(false);
                noodles.SetActive(false);
                sink.SetActive(false);
            }
            if (count == 3)
            {
                instructionSteps.text = insruction[count];
               // StartCoroutine(AudioDelay());
                anchorCube.SetActive(true);
                stove.SetActive(false);
                fridge.SetActive(false);
                sauce.SetActive(true);
                noodles.SetActive(false);
                sink.SetActive(false);
            }
            if (count == 4)
            {
                instructionSteps.text = insruction[count];
                //StartCoroutine(AudioDelay());
                anchorCube.SetActive(true);
                stove.SetActive(true);
                fridge.SetActive(false);
                sauce.SetActive(false);
                noodles.SetActive(true);
                sink.SetActive(true);
            }
            if (count == 5)
            {
                instructionSteps.text = insruction[count];
                //StartCoroutine(AudioDelay());
                anchorCube.SetActive(true);
                stove.SetActive(true);
                fridge.SetActive(false);
                sauce.SetActive(false);
                noodles.SetActive(false);
                sink.SetActive(false);
            }
            count++;
        }

    }

    IEnumerator AudioDelay()
    {
        yield return new WaitForSeconds(2);
        TexttoSpeech.instance.SpeechPlayback();
        audio.Play();
    }
    #endregion

    #region private methods
    private void Start()
    {
        Debug.Log("length" + insruction.Length);
        count = 0;
        if (count == 0)
        {
            instructionSteps.text = insruction[count];
            count++;
        }
    }
    #endregion
}
