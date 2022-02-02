using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator CatAnime;
    //public GameObject cat;
    // Start is called before the first frame update

    private void Start()
    {
        CatAnime = GetComponent<Animator>();
    }
    public void StartVoiceCommand()
    {
        CatAnime.enabled = true;
    }

    // Update is called once per frame
    public void StopVoiceCommand()
    {
        CatAnime.enabled = false;
    }
}
