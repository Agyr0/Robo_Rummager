using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler: MonoBehaviour
{
    public static Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();

    private static int defaultPoolSize = 20;
    private static GameObject poolParent, specificPool;


    //Creates an object pool of size 50s
    public static void MakeNewObjectPool(GameObject go)
    {
        if (objectPools.ContainsKey(go.name))
            return;

        if (poolParent == null)
        {
            poolParent = new GameObject();
            poolParent.name = "Object Pools";
            DontDestroyOnLoad(poolParent);
        }

        specificPool = Instantiate(new GameObject("Specific Pool"), poolParent.transform);
        specificPool.transform.position = Vector3.zero;
        specificPool.name = go.name + " Pool";

        DontDestroyOnLoad(specificPool);

        GameObject temp;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < defaultPoolSize; i++)
        {
            temp = Instantiate(go);
            temp.transform.parent = specificPool.transform;
            temp.SetActive(false);
            list.Add(temp);
        }
        DontDestroyOnLoad(specificPool);
        objectPools.Add(go.name, list);
    }

    //Creates an object pool of a specified size
    public static void MakeNewObjectPool(GameObject go, int poolSize)
    {
        if (objectPools.ContainsKey(go.name))
            return;

        if (poolSize <= 0)
            return;

        if (poolParent == null)
        {
            poolParent = new GameObject();
            poolParent.name = "Object Pools";
            DontDestroyOnLoad(poolParent);
        }

        specificPool = Instantiate(new GameObject("Specific Pool"), poolParent.transform);
        specificPool.transform.position = Vector3.zero;
        specificPool.name = go.name + " Pool";

        DontDestroyOnLoad(specificPool);

        GameObject temp;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(go);
            temp.transform.parent = specificPool.transform;
            temp.SetActive(false);
            list.Add(temp);
        }
        DontDestroyOnLoad(specificPool);
        objectPools.Add(go.name, list);
    }

    //Returns an object if it has a current pool
    public static GameObject PullObjectFromPool(GameObject go)
    {
        if (objectPools.ContainsKey(go.name))
        {
            List<GameObject> tempList = new List<GameObject>();
            objectPools.TryGetValue(go.name, out tempList);

            //If the object does not have a list, make a new object list and pull it
            if (tempList == null)
            {
                MakeNewObjectPool(go);
                return null;
            }
            else
            {
                foreach (GameObject gameObject in tempList)
                {
                    if (gameObject.activeInHierarchy == false)
                    {
                        gameObject.SetActive(true);
                        return gameObject;
                    }
                }
            }

            //If no objects are available to pull, make a new one and add it to the list
            GameObject temp = Instantiate(go);
            tempList.Add(temp);
            temp.transform.parent = tempList[0].transform.parent;

            return temp;
        }
        else
        {
            MakeNewObjectPool(go);
            return PullObjectFromPool(go);
        }
    }
}
