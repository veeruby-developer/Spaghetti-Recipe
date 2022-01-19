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

    
    [Header("ToolTip Objects")]
    [SerializeField] public GameObject stove;
    [SerializeField] public GameObject fridge;
    [SerializeField] public GameObject sauce;
    [SerializeField] public GameObject noodles;
    [SerializeField] public GameObject sink;

    int count;

    #region Public Methods

    //Loading mode page
    public void LoadMainMenu()
    {
        mainPage.SetActive(false);
        modePage.SetActive(true);
    }

    //Loading mode page
    public void HomeFunction()
    {
        anchorCube.SetActive(false);
        togglePage.SetActive(false);
        homeTogglePage.SetActive(false);
        recipeInstructionPage.SetActive(false);
        modePage.SetActive(true);
    }

    //Setup mode button function
    public void StartSetupMode()
    {
        modePage.SetActive(false);
        instructionPage.SetActive(true);
    }

    //Function for instruction page 
    public void InstructionPageFunction()
    {
        instructionPage.SetActive(false);
        anchorCube.SetActive(true);
        togglePage.SetActive(true);
    }

    //Recipe mode button function
    public void StartRecipeMode()
    {
        modePage.SetActive(false);
        anchorCube.SetActive(true);
        recipeInstructionPage.SetActive(true);

        //Finds all the children anchor of cube and making it disable in recipe mode , hence it will be enable according to the flow
        Transform[] allChildren = anchorCube.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.tag == "Child")
            {
                child.gameObject.SetActive(false);
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

    //Function for disabling the eyegaze
    public void DisableEyeTracking()
    {
        eyeMessagePanel.SetActive(false);
    }

    //Next function for recipe mode
    public void NextFunction()
    {
        if (count < insruction.Length)
        {
            Debug.Log("Count = " + count);
            if (count == 1)
            {
                instructionSteps.text = insruction[count];
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

    #endregion

    #region private methods
    //Initial method to display starting message stored in array
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
