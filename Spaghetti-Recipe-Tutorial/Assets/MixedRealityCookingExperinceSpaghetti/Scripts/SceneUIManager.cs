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

    [Tooltip("Button")]
    [SerializeField] public GameObject backBtn;
    [SerializeField] public GameObject homeBtn;
    [SerializeField] public GameObject nextBtn;

    [Header("Object")]
    [Tooltip("Anchor Cube")]
    [SerializeField] public GameObject anchorCube;

    [Header("Instructions")]
    [Tooltip("Recipe steps")]
    [SerializeField] public string[] insruction;
        
    [Header("Audio Source")]
    [SerializeField] public AudioSource audio;

    GameObject[] tooltipObj;
    bool isTool;

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
        audio.Stop();
        
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
        count++;
        if (count < insruction.Length)
        {
            Debug.Log("NExt Count = " + count);
            if (count == 1)
            {
                instructionSteps.text = insruction[count];
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
            Debug.Log("back = " + count);
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
    #endregion

#region private methods
    //Initial method to display starting message stored in array
    void Start()
    {
        isTool = true;
        count = 0;
        if (count == 0)
        {
            instructionSteps.text = insruction[count];
        }
    }

    private void Update()
    {
        if (count == 0)
        {
            backBtn.SetActive(false);
            homeBtn.SetActive(false);
            nextBtn.SetActive(true);
        }
        if(count <5 && count != 0)
        {
            backBtn.SetActive(true);
            homeBtn.SetActive(false);
            nextBtn.SetActive(true);
        }
        if (count == 5)
        {
            backBtn.SetActive(true);
            homeBtn.SetActive(true);
            nextBtn.SetActive(false);
        }
        if(anchorCube.activeInHierarchy == true && isTool == true)
        {
            tooltipObj = GameObject.FindGameObjectsWithTag("Child");
            isTool = false;
        }
        
    }
    #endregion
}
