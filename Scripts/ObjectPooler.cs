using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    public List<Pool> pools = new List<Pool>();

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton

    public static ObjectPooler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                Transform spawnedObject = Instantiate(pool.prefab, transform.position, Quaternion.identity);

                spawnedObject.SetParent(transform);

                spawnedObject.gameObject.SetActive(false);

                objects.Enqueue(spawnedObject.gameObject);
            }

            poolDictionary.Add(pool.id, objects);
        }
    }

    public GameObject SpawnFromPool(string poolId, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolId))
        {
            Debug.LogError("Pool with tag " + poolId + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[poolId].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (objectToSpawn.GetComponent<IPooledObject>() != null)
            objectToSpawn.GetComponent<IPooledObject>().OnObjectSpawn();

        poolDictionary[poolId].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnFromPool(string poolId, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(poolId))
        {
            Debug.LogError("Pool with tag " + poolId + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[poolId].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(parent);

        if (objectToSpawn.GetComponent<IPooledObject>() != null)
            objectToSpawn.GetComponent<IPooledObject>().OnObjectSpawn();

        poolDictionary[poolId].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    

}

[System.Serializable]
public class Pool
{

    public string id;

    public Transform prefab;

    public int size;

}