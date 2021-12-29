using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashText : MonoBehaviour
{
   public GameObject splashText;
   public GameObject uiMenu;
    bool isSplash;
    // Start is called before the first frame update
    void Start()
    {
        isSplash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSplash)
        {
            Debug.Log("IsSplash");
            if (splashText.GetComponent<MeshRenderer>().enabled == false)
            {
                SplashEnd();
                isSplash = false;
                Debug.Log("End");
            }
        }
        
    }

    public void SplashEnd()
    {
        splashText.SetActive(false);
        uiMenu.SetActive(true);
    }
}
