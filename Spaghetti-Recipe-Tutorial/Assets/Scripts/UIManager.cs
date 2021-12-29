using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  

    [SerializeField] public GameObject welcomePanel, modePanel, instructionPanel, anchorCube;

    // Start is called before the first frame update


    void WelcomeStartFunction()
    {
        welcomePanel.SetActive(false);
        modePanel.SetActive(true);
    }
    void SetupModeFunction()
    {
        modePanel.SetActive(false);
        instructionPanel.SetActive(true);
    }
    void InstructionPanelFunction()
    {
        instructionPanel.SetActive(false);
        anchorCube.SetActive(true);
    }

    
}
