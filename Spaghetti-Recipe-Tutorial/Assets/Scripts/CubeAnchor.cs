using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnchor : MonoBehaviour
{
    public AnchorModuleScript anchorScript;
   // public GameObject mainMenuPanel;
    GameObject cube;

    bool isCreateAnchor, isRemoveAnchor, isFinAnchor,isCreateDone;
    // Start is called before the first frame update
    void Start()
    {
        cube = gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anchorScript.isStart == true && isCreateAnchor == true)
        {
            Debug.Log("if Anchor create bool - " + anchorScript.isStart);
            anchorScript.CreateAzureAnchor(cube);
            anchorScript.isStart = false;
        }

        if (anchorScript.isCreate == true && isCreateAnchor == true)
        {
            Debug.Log("if Anchor stop bool - " + anchorScript.isStart);
            anchorScript.StopAzureSession();
            anchorScript.isCreate = false;
            isCreateAnchor = false;
        }
        //if(anchorScript.isStop == true && isCreateAnchor == true)
        //{
        //    cube.SetActive(false);
        //    mainMenuPanel.SetActive(true);
        //    isCreateAnchor = false;
        //}

        if(anchorScript.isStart == true && isRemoveAnchor == true)
        {
            Debug.Log("Anchor start " + anchorScript.isStart);
            anchorScript.RemoveLocalAnchor(cube);
            anchorScript.isStart = false;
        }

        if(anchorScript.isRemove == true && isRemoveAnchor == true)
        {
            Debug.Log("Anchor remove " + anchorScript.isRemove);
            anchorScript.StopAzureSession();
            anchorScript.isRemove = false;
            isRemoveAnchor = false;
        }



        if (anchorScript.isStart == true && isFinAnchor == true)
        {
            Debug.Log("Anchor start " + anchorScript.isStart);
            anchorScript.GetAzureAnchorIdFromDisk();
            anchorScript.FindAzureAnchor();
            anchorScript.isStart = false;
        }
        if(anchorScript.isFind == true && isFinAnchor == true)
        {
            Debug.Log("Anchor Find " + anchorScript.isStart);
            anchorScript.StopAzureSession();
            anchorScript.isFind = false;
            isFinAnchor = false;
        }
    }

    public void CreateAnchor()
    {
        isCreateAnchor = true;
        Debug.Log("Anchor create bool - " + anchorScript.isStart);
        anchorScript.StartAzureSession();
    }

    public void RemoveAnchor()
    {
        isRemoveAnchor = true;

        anchorScript.StartAzureSession();

    }

    public void FindAnchor()
    {
        isFinAnchor = true;
        anchorScript.StartAzureSession();

        

        //anchorScript.StopAzureSession();
    }

    public void StopAnchor()
    {
        anchorScript.StopAzureSession();
    }
}
