using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    const string COLLECTABLE_SUB = "/collectable";
    const string CCOLLECTABLE_COUNT_SUB = "/collectable.count";
    public static List<Collectable> collectabls = new List<Collectable>();

    [SerializeField] Collectable collectableObject;
    //[SerializeField] bool isEditor;

    //public GameObject pageNavigator;

    public void SaveCollectable()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + COLLECTABLE_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + CCOLLECTABLE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        Debug.Log(path);

        FileStream countStreem = new FileStream(countPath, FileMode.Create);
        
        formatter.Serialize(countStreem, collectabls.Count);
        
        countStreem.Close();
        
        for (int i = 0; i < collectabls.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            CollectableDataClass data = new CollectableDataClass(collectabls[i]);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        //PageNavigator.isEditor = false;
        //collectabls.Clear();
        //pageNavigator.GetComponent<PageNavigator>().clear.SetActive(false);
        //pageNavigator.GetComponent<PageNavigator>().editorPageHeaderAndFooter.SetActive(true);
        //pageNavigator.GetComponent<PageNavigator>().editorPage.SetActive(false);
        //pageNavigator.GetComponent<PageNavigator>().mainpage.SetActive(true);
        //ReloadAllObject();
    }

    public void LoadCollectabls()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + COLLECTABLE_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + CCOLLECTABLE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int collectablesCound = 0;

        if (File.Exists(countPath))
        {
            FileStream countStreem = new FileStream(countPath, FileMode.Open);
            collectablesCound = (int)formatter.Deserialize(countStreem);
            countStreem.Close();
        }
        else
        {
            Debug.Log("No File Exists");
        }

        for (int i = 0; i < collectablesCound; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                CollectableDataClass data = formatter.Deserialize(stream) as CollectableDataClass;
                stream.Close();

                Vector3 size = new Vector3(data.size[0], data.size[1], data.size[2]);

                Collectable collectable = Instantiate(collectableObject, transform.position, Quaternion.identity);
                collectable.transform.localPosition = size;
                //collectable.points = data.points;
                //collectable.pointsText.text = data.points.ToString();
                collectable.modelName = data.modelName;
                //collectable.anchorName = data.anchorName;
                //collectable.isAnchored = data.isAnchored;
                collectable.transform.Find(collectable.modelName).transform.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("No Data Found");
            }

        }
    }
    //public void ClearList()
    //{
    //    Debug.Log("Saved List Cleared");
    //    foreach (var collectable in collectabls)
    //    {
    //        Destroy(collectable.transform.gameObject);
    //    }
    //    collectabls.Clear();
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string countPath = Application.persistentDataPath + CCOLLECTABLE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
    //    FileStream countStreem = new FileStream(countPath, FileMode.Create);
    //    formatter.Serialize(countStreem, collectabls.Count);
    //    countStreem.Close();
    //    //pageNavigator.GetComponent<PageNavigator>().clear.SetActive(false);
    //    //pageNavigator.GetComponent<PageNavigator>().editorPage.SetActive(true);
    //    //pageNavigator.GetComponent<PageNavigator>().editorPageHeaderAndFooter.SetActive(true);
        
    //}

    //public void ReloadAllObject()
    //{
    //    foreach (var collectable in GameObject.FindGameObjectsWithTag("collectable"))
    //    {
    //        Destroy(collectable);
    //    }
    //    collectabls.Clear();
    //}


    //public void EnableEditMode()
    //{
    //    LoadCollectabls();
    //}

    //public void EnablePlayMode()
    //{
    //    LoadCollectabls();
    //    GameObject[] editorUI = GameObject.FindGameObjectsWithTag("noneditor");
    //    foreach (GameObject editorUIs in editorUI)
    //    {
    //        editorUIs.SetActive(false);
    //    }

    //    StartCoroutine("EnableTriggerOnModel");
    //}

    //public IEnumerator EnableTriggerOnModel()
    //{
    //    yield return new WaitForSeconds(4.5f);

    //    GameObject[] pointsObjects = GameObject.FindGameObjectsWithTag("points");
    //    foreach (GameObject pointsObject in pointsObjects)
    //    {
    //        pointsObject.GetComponent<BoxCollider>().isTrigger = true;
    //    }
    //}

}
