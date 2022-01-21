using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFunctions : MonoBehaviour
{
    //Object change declaration
    [Header("Objects Mesh Filter")]
    public MeshFilter CubeMesh;
    [Header("Object Mesh List")]
    public Mesh[] objects;
    private int ObjectValue;

    //Color change declaration
    [Header("Objects Mesh Renderer")]
    public Renderer ObjectRenderer;
    public GameObject Cube;
    [Header("Color List")]
    [SerializeField] private Color[] colors;
    private int ColorValue;

    //Eye Gaze declaration
    Vector3 temp;
    bool isGazedOn, isGazedOff;

    //Object change  logic
    public void ChangeObject()
    {
        Debug.Log(ObjectValue);
        
        if (ObjectValue > 3)
        {
            ObjectValue = 0;
        }
        CubeMesh.mesh = objects[ObjectValue];
        ObjectValue++;
    }

    // Cube color change 
    void Start()
    {
        ObjectRenderer = Cube.GetComponent<Renderer>();
    }

    public void ColorChange()
    {
        ColorValue++;
        if (ColorValue > 2)
        {
            ColorValue = 0;
        }
        ObjectRenderer.material.color = colors[ColorValue];
    }

    // Eye gaze logic
    void Update()
    {
        //Object increase it's size when button is gazed 
        if (isGazedOn == true)
        {
            temp = CubeMesh.gameObject.transform.localScale;

            if (temp.x <= 1 && temp.y <= 1 && temp.z <= 1)
            {
                temp.x += 0.001f;
                temp.y += 0.001f;
                temp.z += 0.001f;
            }
            CubeMesh.gameObject.transform.localScale = temp;
        }
        //Object decreases it's size when button is gazed 
        if (isGazedOff == true)
        {
            temp = CubeMesh.gameObject.transform.localScale;
            if (temp.x >= 0.5f && temp.y >= 0.5f && temp.z >= 0.5f)
            {
                temp.x -= 0.001f;
                temp.y -= 0.001f;
                temp.z -= 0.001f;
            }
            CubeMesh.gameObject.transform.localScale = temp;
        }
    }

    //On eye gazing function
    public void OnGaze()
    {
        isGazedOn = true;
        isGazedOff = false;
    }

    //Off eye gazing function
    public void OffGaze()
    {
        isGazedOn = false;
        isGazedOff = true;
    }
}
