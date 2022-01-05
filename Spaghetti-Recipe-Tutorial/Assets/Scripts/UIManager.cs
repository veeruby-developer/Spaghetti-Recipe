using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] public GameObject welcomePanel;
    [SerializeField] public GameObject mainMenuPanel;
    [SerializeField] public GameObject instructionPanel;
    [SerializeField] public GameObject togglePanel;
    [SerializeField] public GameObject anchorCube;

    // Start is called before the first frame update


    public void WelcomeStartFunction()
    {
        welcomePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void EnableSetupMode()
    {
        mainMenuPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }
    public void InstructionPanelFunction()
    {
        instructionPanel.SetActive(false);
        anchorCube.SetActive(true);
        togglePanel.SetActive(true);
    }

    public void HomeFunction()
    {
        togglePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void EnableRecipeMode()
    {
        mainMenuPanel.SetActive(false);
        togglePanel.SetActive(true);
        GameObject child_1 = togglePanel.transform.GetChild(1).gameObject;
        GameObject child_2 = togglePanel.transform.GetChild(2).gameObject;
        child_1.SetActive(false);
        child_2.SetActive(false);
    }

}
