using UnityEngine;


[System.Serializable]
public class CollectableDataClass
{
    //public int points;
    public string modelName;
    //public string anchorName;
    public float[] size = new float[3];
    //public bool isAnchored;

    public CollectableDataClass(Collectable collectable)
    {
        //points = collectable.points;
        modelName = collectable.modelName;
        //anchorName = collectable.anchorName;
        //isAnchored = collectable.isAnchored;
        Vector3 collectableSize = collectable.transform.localScale;
        size[0] = collectableSize.x;
        size[1] = collectableSize.y;
        size[2] = collectableSize.z;
    }
}
