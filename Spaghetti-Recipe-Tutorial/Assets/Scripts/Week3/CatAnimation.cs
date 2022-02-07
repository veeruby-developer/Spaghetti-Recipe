using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    //Animator declaration 
    private Animator CatAnime;

    private void Start()
    {
        CatAnime = GetComponent<Animator>();
    }

    //Start Animation voice command function
    
    public void StartVoiceCommand()
    {
        CatAnime.SetBool("AnimActive", true);
    }

    //Stop Animation voice command function
    public void StopVoiceCommand()
    {
        //CatAnime.enabled = false;
        CatAnime.SetBool("AnimActive", false);
    }
}
