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
    public GameObject voiceMessagePanel;
    [SerializeField]
    public GameObject eyeMessagePanel;

    [Tooltip("UI text")]
    public TextMeshProUGUI voiceMessage, eyeMessage;

    
    #region Public Methods

    //Function for welcome panel button
    public void LoadMainPage()
    {
        mainPage.SetActive(false);
        modePage.SetActive(true);
    }

       
    //Setup mode button function with popup messages
    public void StartSetupMode()
    {
        voiceMessage.text = "Setup Mode Loaded";
        voiceMessagePanel.SetActive(true);
        modePage.SetActive(false);
        StartCoroutine(Setup());
    }
    //Set delay time for message panel to disappear 
    IEnumerator Setup()
    {
        yield return new WaitForSeconds(3);
        voiceMessagePanel.SetActive(false);
        modePage.SetActive(true);
    }

    //Recipe mode button function with popup messages
    public void StartRecipeMode()
    {
        voiceMessage.text = "Recipe Mode Loaded";
        voiceMessagePanel.SetActive(true);
        modePage.SetActive(false);
        StartCoroutine(Recipe());
    }
    //Set delay time for message panel to disappear 
    IEnumerator Recipe()
    {
        yield return new WaitForSeconds(3);
        voiceMessagePanel.SetActive(false);
        modePage.SetActive(true);
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
    #endregion
}
