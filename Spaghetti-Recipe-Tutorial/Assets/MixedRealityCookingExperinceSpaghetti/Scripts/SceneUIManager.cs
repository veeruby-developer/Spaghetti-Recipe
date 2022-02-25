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
    public GameObject setupInstructionPage;
    [SerializeField]
    public GameObject AnchorMode;
    [SerializeField]
    public GameObject eyeMessagePanel;
    [SerializeField]
    public GameObject recipeInstructionPage;

    [Tooltip("UI text")]
    [SerializeField]public TextMeshProUGUI eyeMessage;
    [SerializeField]public TextMeshProUGUI instructionSteps;

    [Tooltip("Button")]
    [SerializeField] public GameObject backBtn;
    [SerializeField] public GameObject homeBtn;
    [SerializeField] public GameObject nextBtn;

    [Header("Object")]
    [Tooltip("Anchor Cube")]
    [SerializeField] public GameObject anchorCube;

    
    public AnchorObject ObjectAnchor;

    [Header("Instructions")]
    [Tooltip("Recipe steps")]
    [SerializeField] public string[] insruction;

    [Header("Tool Tips")]
    [Tooltip("Tool Tips")]
    public GameObject[] tooltipObj;

    [Header("Audio Source")]
    [SerializeField] public AudioSource audio;


    // To keep track of which Instruction is being played
    private int count;

    #region Public Methods

    //Loading mode page
    public void LoadMainMenu()
    {
        mainPage.SetActive(false);
        modePage.SetActive(true);
    }

    //Setup mode button function
    public void StartSetupMode()
    {
        modePage.SetActive(false);
        setupInstructionPage.SetActive(true);
        anchorCube.SetActive(true);
    }

    //Function to Enable Anchor Mode
    public void EnableAnchorMode()
    {
        setupInstructionPage.SetActive(false);
        AnchorMode.SetActive(true);
        StartSession();
    }

    //Loading End Setup Mode
    public void EndSetupMode()
    {
        anchorCube.SetActive(false);
        AnchorMode.SetActive(false);
        modePage.SetActive(true);

    }

    //Recipe mode button function
    public void StartRecipeMode()
    {
        anchorCube.SetActive(true);
        nextBtn.SetActive(true);
        homeBtn.SetActive(false);
        backBtn.SetActive(false);
        modePage.SetActive(false);
        recipeInstructionPage.SetActive(true);
        count = 0;
        instructionSteps.text = insruction[count];
        foreach (GameObject tooltip in tooltipObj)
        {
            tooltip.gameObject.SetActive(false);
        }
        Invoke("StartSession", 1f);
        Invoke("FindAnchor", 2f);
    }

    //Loading End Recipy Mode
    public void EndRecipyMode()
    {
        anchorCube.SetActive(false);
        recipeInstructionPage.SetActive(false);
        modePage.SetActive(true);
        audio.Stop();
        ObjectAnchor.StopAzureSession();
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
        count++;
        if (count < insruction.Length)
        {
            if (count == 1)
            {
                backBtn.SetActive(true);
                instructionSteps.text = insruction[count];
                Debug.Log(tooltipObj);
                for(int i = 0; i<tooltipObj.Length; i++)
                {
                    if(tooltipObj[i].name == "Stove")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
                
                
            }
            if (count == 2)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Fridge")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
                
                
            }
            if (count == 3)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Spaghetti Sauce")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
                
                
            }
            if (count == 4)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Stove" || tooltipObj[i].name == "Sink" || tooltipObj[i].name == "Noodles")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
                
                
            }
            if (count == 5)
            {
                backBtn.SetActive(true);
                homeBtn.SetActive(true);
                nextBtn.SetActive(false);
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Stove")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
                
            }
            
        }
        
    }

    // Back Function
    public void BackFunction()
    {
        count--;
        if (count >= 0)
        {
            if (count == 0)
            {
                backBtn.SetActive(false);
                homeBtn.SetActive(false);
                nextBtn.SetActive(true);
                instructionSteps.text = insruction[count];
            }
            if (count == 1)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Stove")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
            }
            if (count == 2)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Fridge")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }

            }
            if (count == 3)
            {
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Spaghetti Sauce")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
            }
            if (count == 4)
            {
                homeBtn.SetActive(false);
                nextBtn.SetActive(true);
                instructionSteps.text = insruction[count];
                for (int i = 0; i < tooltipObj.Length; i++)
                {
                    if (tooltipObj[i].name == "Stove" || tooltipObj[i].name == "Sink" || tooltipObj[i].name == "Noodles")
                    {
                        tooltipObj[i].SetActive(true);
                    }
                    else
                    {
                        tooltipObj[i].SetActive(false);
                    }
                }
          
            }
        }
        
        
    }
    #endregion

    #region ASA Functions


    // Create Anchor to Object
    public void CreateAncor()
    {
        ObjectAnchor.CreateAzureAnchor(anchorCube);
    }

    // Remove Local Anchor
    public void RemoveLocalAnchor()
    {
        ObjectAnchor.RemoveLocalAnchor(anchorCube);
    }

    public void FindAnchor()
    {
        ObjectAnchor.FindAzureAnchor();
    }

    public void StartSession()
    {
        ObjectAnchor.StartAzureSession();
    }

    public void StopSession()
    {
        ObjectAnchor.StopAzureSession();
    }


    #endregion

}
