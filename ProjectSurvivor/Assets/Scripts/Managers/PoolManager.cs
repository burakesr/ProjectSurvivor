using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    public List<Pool> pools = new List<Pool>();
    public Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            CreatePool(pools[i].prefab, pools[i].size);
        }
    }

    public void CreatePool(GameObject prefab, int size)
    {
        int poolKey = prefab.GetInstanceID();

        GameObject poolHolder = new GameObject(prefab.name + " Pool");
        poolHolder.transform.SetParent(transform);

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            for (int x = 0; x < size; x++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.transform.SetParent(poolHolder.transform);
            }
        }

    }

    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToSpawn = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToSpawn);

            objectToSpawn.Reuse(position, rotation);

            return objectToSpawn.gameObject;
        }

        return null;
    }
}

public class ObjectInstance
{
    public GameObject gameObject;
    public Transform transform;

    private bool hasPoolObjectComponent;
    private IPoolObject poolObject;

    public ObjectInstance(GameObject objectInstance)
    {
        gameObject = objectInstance;
        transform = gameObject.transform;
        gameObject.SetActive(false);

        if (gameObject.GetComponent<IPoolObject>() != null)
        {
            hasPoolObjectComponent = true;
            poolObject = gameObject.GetComponent<IPoolObject>();
        }
    }

    public void Reuse(Vector3 position, Quaternion rotation)
    {
        if (hasPoolObjectComponent)
        {
            poolObject.OnObjectReuse();
        }

        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }
}

[Serializable]
public class Pool
{
    public GameObject prefab;
    public int size;
}
