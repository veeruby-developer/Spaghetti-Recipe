using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange : MonoBehaviour
{
    [SerializeField] Renderer CubeRenderer;
    [SerializeField] Renderer SphereRenderer;
    [SerializeField] Renderer CylinderRenderer;
    public GameObject Cube, Sphere, Cylinder;
    private int ColorValue;
    // Start is called before the first frame update
    void Start()
    {
        CubeRenderer = Cube.GetComponent<Renderer>();
        SphereRenderer = Sphere.GetComponent<Renderer>();
        CylinderRenderer = Cylinder.GetComponent<Renderer>();

    }

    public void ChangeObject()
    {
        
    }
}
