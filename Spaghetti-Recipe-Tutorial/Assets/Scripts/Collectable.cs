using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    //public int points = 10;
    public string modelName;
    //public string anchorName;
    //public Text pointsText;
    //public bool isAnchored = true;

    //[Header("Random Anchor Names")]
    //public string[] adjectives;
    //public string[] nouns;
    //public string[] verbs;
    //public string[] animals;

    //[SerializeField] AudioSource buttonClick;

    //public void Awake()
    //{
    //    isAnchored = false;
    //}

    public void Start()
    {
        //if (PageNavigator.isEditor == true)
        //{
            SaveSystem.collectabls.Add(this);
        //Debug.Log(transform.localPosition);
        //}
        //pointsText.text = points.ToString();

    }

    //public void AddtoSaveList()
    //{
    //    SaveSystem.collectabls.Add(this);
    //}

    //public void DeleteCurrentModel()
    //{
    //    GameObject.Find("Main Canvas").GetComponent<AudioSource>().Play();
    //    SaveSystem.collectabls.Remove(this);
    //    Destroy(gameObject);
    //}

    //public string GenerateName()
    //{
    //    return (adjectives[Random.Range(0, adjectives.Length)] + nouns[Random.Range(0, nouns.Length)] + verbs[Random.Range(0, nouns.Length)] + animals[Random.Range(0, nouns.Length)]).ToLower();
    //}

    //public void IncrementPoints()
    //{
    //    points = points + 10;
    //    buttonClick.Play();
    //    pointsText.text = points.ToString();
    //}

    //public void DecrimentPoints()
    //{
    //    points = points - 10;
    //    buttonClick.Play();
    //    pointsText.text = points.ToString();
    //}

    //public void EnableBoundsCountrol()
    //{
    //    buttonClick.Play();
    //    gameObject.GetComponent<BoundsControl>().enabled = true;
    //}

}
