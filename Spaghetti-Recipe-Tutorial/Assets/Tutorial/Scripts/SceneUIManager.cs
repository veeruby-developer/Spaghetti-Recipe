using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    [Tooltip("UI panels")]
    [SerializeField] public GameObject welcomePanel, modePanel, instructionPanel, anchorCube;

    #region Public Methods

    public void WelcomeStartFunction()
    {
        welcomePanel.SetActive(false);
        modePanel.SetActive(true);
    }
    public void SetupModeFunction()
    {
        modePanel.SetActive(false);
        instructionPanel.SetActive(true);
    }
    public void InstructionPanelFunction()
    {
        instructionPanel.SetActive(false);
        anchorCube.SetActive(true);
    }
    #endregion
}
