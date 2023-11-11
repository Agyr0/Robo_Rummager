using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depricated
{

    [DefaultExecutionOrder(1)]
    public class OldObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;
        public List<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int maxObjects;

        void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            GameObject parent = new GameObject(objectPrefab.name + " Pooled Container");
            for (int i = 0; i < maxObjects; i++)
            {
                tmp = Instantiate(objectPrefab, parent.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < maxObjects; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }

}