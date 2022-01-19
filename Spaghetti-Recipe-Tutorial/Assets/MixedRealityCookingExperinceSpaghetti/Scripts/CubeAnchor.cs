using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnchor : MonoBehaviour
{
    //AnchorModuleScript to be assigned
    public AnchorModuleScript anchorScript;

    //private gameobject of cube
    GameObject cube;

    //nool function to check the script condidtions
    bool isCreateAnchor, isRemoveAnchor, isFinAnchor,isCreateDone;

    // Start is called before the first frame update 
    void Start()
    {
        cube = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Condition to check the start anchor process is done and that starts the create anchor - create anchor function
        if (anchorScript.isStart == true && isCreateAnchor == true)
        {
            Debug.Log("if Anchor create bool - " + anchorScript.isStart);
            anchorScript.CreateAzureAnchor(cube);
            anchorScript.isStart = false;
        }
        //Condition to check the create anchor process is done and that starts the stop anchor - create anchor function
        if (anchorScript.isCreate == true && isCreateAnchor == true)
        {
            Debug.Log("if Anchor stop bool - " + anchorScript.isStart);
            anchorScript.StopAzureSession();
            anchorScript.isCreate = false;
            isCreateAnchor = false;
        }

        //Condition to check the start anchor process is done and that starts the remove anchor - remove anchor function
        if (anchorScript.isStart == true && isRemoveAnchor == true)
        {
            Debug.Log("Anchor start " + anchorScript.isStart);
            anchorScript.RemoveLocalAnchor(cube);
            anchorScript.isStart = false;
        }
        //Condition to check the remove anchor process is done and that starts the stop anchor - remove anchor function
        if (anchorScript.isRemove == true && isRemoveAnchor == true)
        {
            Debug.Log("Anchor remove " + anchorScript.isRemove);
            anchorScript.StopAzureSession();
            anchorScript.isRemove = false;
            isRemoveAnchor = false;
        }


        //Condition to check the start anchor process is done and that starts the find anchor - Find anchor function
        if (anchorScript.isStart == true && isFinAnchor == true)
        {
            Debug.Log("Anchor start " + anchorScript.isStart);
            anchorScript.FindAzureAnchor();
            anchorScript.isStart = false;
        }
        //Condition to check the find anchor process is done and that starts the stop anchor - Find anchor function
        if (anchorScript.isFind == true && isFinAnchor == true)
        {
            Debug.Log("Anchor Find " + anchorScript.isStart);
            anchorScript.StopAzureSession();
            anchorScript.isFind = false;
            isFinAnchor = false;
        }
    }

    //Create Anchor function
    public void CreateAnchor()
    {
        isCreateAnchor = true;
        anchorScript.StartAzureSession();
    }

    //Remove Anchor function
    public void RemoveAnchor()
    {
        isRemoveAnchor = true;
        anchorScript.StartAzureSession();
    }

    //Find Anchor function
    public void FindAnchor()
    {
        isFinAnchor = true;
        anchorScript.StartAzureSession();
    }

}
