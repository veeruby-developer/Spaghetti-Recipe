using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange : MonoBehaviour
{

    public MeshFilter CubeMesh;
    //public GameObject cube;
    public Mesh[] objects;
    private int ObjectValue;

    public void ChangeObject()
    {
       
        if (ObjectValue > 3)
        {
            ObjectValue = 0;
        }
        CubeMesh.mesh = objects[ObjectValue];
        ObjectValue++;
    }
}
