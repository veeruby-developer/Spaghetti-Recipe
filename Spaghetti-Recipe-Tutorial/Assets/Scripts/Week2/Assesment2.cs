using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assesment2 : MonoBehaviour
{
    public Renderer ObjectRenderer;
    public GameObject Cube;
    [SerializeField] private Color[] colors;
    private int ColorValue;
    void Start()
    {
        ObjectRenderer = Cube.GetComponent<Renderer>();
    }

    // Cube color change on voice command
    public void ColorChange()
    {
        ColorValue++;
        if(ColorValue > 2)
        {
            ColorValue = 0;
        }
        ObjectRenderer.material.color = colors[ColorValue];
    }

    public void ChangeObject()
    {

    }
}
