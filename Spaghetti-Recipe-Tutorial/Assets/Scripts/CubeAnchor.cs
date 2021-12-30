using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnchor : MonoBehaviour
{
    public AnchorModuleScript anchorScript;
    GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        cube = gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAnchor()
    {
        Debug.Log("Anchor create bool - " + anchorScript.isStart);
        anchorScript.StartAzureSession();
        if(anchorScript.isStart == true)
        {
            Debug.Log("if Anchor create bool - " + anchorScript.isStart);
            anchorScript.CreateAzureAnchor(cube);
            anchorScript.isStart = false;
        }
        

       // anchorScript.StopAzureSession();
    }

    public void RemoveAnchor()
    {
        anchorScript.StartAzureSession();

        anchorScript.RemoveLocalAnchor(cube);

        anchorScript.StopAzureSession();
    }

    public void FindAnchor()
    {
        anchorScript.StartAzureSession();

        anchorScript.FindAzureAnchor();

        //anchorScript.StopAzureSession();
    }

    public void StopAnchor()
    {
        anchorScript.StopAzureSession();
    }
}
