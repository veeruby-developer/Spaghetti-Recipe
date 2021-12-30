using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneUIManager : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("UI panels")]
    [SerializeField] public GameObject welcomePanel, modePanel, instructionPanel, voiceMessagePanel, eyeMessagePanel;
    [Tooltip("UI text")]
    public TextMeshProUGUI voiceMessage, eyeMessage;

    [Tooltip("Anchor Object")]
    [SerializeField] public GameObject anchorCube;

    #region Public Methods

    //Function for welcome panel button
    public void WelcomeStartFunction()
    {
        welcomePanel.SetActive(false);
        modePanel.SetActive(true);
    }

    //SetupMode button function for futher flow
    public void SetupModeFunction()
    {
        modePanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    //Instruction panel button in setupmode 
    public void InstructionPanelFunction()
    {
        instructionPanel.SetActive(false);
        anchorCube.SetActive(true);
    }

    //Setup mode button function with popup messages
    public void SetupMode()
    {
        voiceMessage.text = "Setup Mode Loaded";
        voiceMessagePanel.SetActive(true);
        modePanel.SetActive(false);
        StartCoroutine(Setup());
    }
    //Set delay time for message panel to disappear 
    IEnumerator Setup()
    {
        yield return new WaitForSeconds(3);
        voiceMessagePanel.SetActive(false);
        modePanel.SetActive(true);
    }

    //Recipe mode button function with popup messages
    public void RecipeMode()
    {
        voiceMessage.text = "Recipe Mode Loaded";
        voiceMessagePanel.SetActive(true);
        modePanel.SetActive(false);
        StartCoroutine(Recipe());
    }
    //Set delay time for message panel to disappear 
    IEnumerator Recipe()
    {
        yield return new WaitForSeconds(3);
        voiceMessagePanel.SetActive(false);
        modePanel.SetActive(true);
    }

    //Function for eyegaze on setup mode
    public void EyeGazeFunction_SetupModeON()
    {
        eyeMessage.text = "This mode helps you to setup the cooking experince";
        eyeMessagePanel.SetActive(true);
    }
    //Function for eyegaze on recipe mode
    public void EyeGazeFunction_RecipeModeON()
    {
        eyeMessage.text = "This mode helps you to cook the recipe";
        eyeMessagePanel.SetActive(true);
    }
    public void EyeGazeFunctionOff()
    {
        eyeMessagePanel.SetActive(false);
    }
    #endregion
}
