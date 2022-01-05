using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeMode : MonoBehaviour
{
    [Header("UI TextMeshPro")]
    [Tooltip("Instruction Text")]
    [SerializeField] public TextMeshProUGUI instructionSteps;
    [SerializeField] public string[] insruction;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        if (count == 0)
        {
            instructionSteps.text = insruction[count];
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextFunction()
    {
        if(count <= insruction.Length)
        {
            Debug.Log("Count = " + count);
            if (count == 1)
            {
                instructionSteps.text = insruction[count];
                TexttoSpeech.instance.SpeechPlayback();

            }
            if (count == 2)
            {
                instructionSteps.text = insruction[count];
            }
            if (count == 3)
            {
                instructionSteps.text = insruction[count];
            }
            if (count == 4)
            {
                instructionSteps.text = insruction[count];
            }
            count++;
        }
        
    }

   }
