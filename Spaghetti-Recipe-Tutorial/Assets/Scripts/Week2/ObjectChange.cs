using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange : MonoBehaviour
{
   
    public GameObject Cube, Sphere, Cylinder;
    //private GameObject[] cubes = new GameObject[3];
    private int ObjectValue;

    public void ChangeObject()
    {
        if (ObjectValue == 0)
        {
            Cube.SetActive(true);
            Sphere.SetActive(false);
            Cylinder.SetActive(false);
        }
        else if (ObjectValue == 1)
        {
            Sphere.SetActive(true);
            Cylinder.SetActive(false);
            Cube.SetActive(false);
        }
        else if (ObjectValue == 2)
        {
            Cylinder.SetActive(true);
            Cube.SetActive(false);
            Sphere.SetActive(false);

        }
        ObjectValue++;
        if (ObjectValue > 2)
        {
            ObjectValue = 0;
        }
    }
}
