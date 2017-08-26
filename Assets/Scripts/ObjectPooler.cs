using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler SharedInstance;

    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Use this for initialization
    void OnEnable()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> getPooledObjects(string tag, int num)
    {
        List<GameObject> pooledObjs = new List<GameObject>();
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                pooledObjs.Add(pooledObjects[i]);
            }
        }
        
        //3
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {

                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    pooledObjs.Add(obj);
                }
            }
        }
        if(pooledObjs.Count != 0)
        {
            return pooledObjs;
        }
        return null;

    }

    public GameObject getPooledObject(string tag)
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        //3
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {

                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;

    }
}
