using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGraze : MonoBehaviour
{
    //public float speed = 1f;
    Vector3 temp;
    bool isGazedOn,isGazedOff;
    

    // Update is called once per frame
    void Update()
    {

        if(isGazedOn == true)
        {
            temp = gameObject.transform.localScale;

            if (temp.x <= 1.5 && temp.y <= 1.5 && temp.z <= 1.5)
            {
                temp.x += 0.001f;
                temp.y += 0.001f;
                temp.z += 0.001f;
            }
            transform.localScale = temp;
        }
        if (isGazedOff == true)
        {
            temp = gameObject.transform.localScale;
            if (temp.x >= 0.5f && temp.y >= 0.5f && temp.z >= 0.5f)
            {
                temp.x -= 0.001f;
                temp.y -= 0.001f;
                temp.z -= 0.001f;
            }
            transform.localScale = temp;
        }
    }

    public void OnGaze()
    {
        isGazedOn = true;
        isGazedOff = false;
      
        
    }

    public void OffGaze()
    {
        isGazedOn = false;
        isGazedOff = true;
       
        
    }
}
