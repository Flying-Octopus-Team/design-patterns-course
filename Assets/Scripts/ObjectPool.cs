using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Stack<TimeKiller> pool;

    public static ObjectPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        pool = new Stack<TimeKiller>();
    }

    public TimeKiller Spawn(TimeKiller prefab, Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            var spawnedObject = Instantiate(prefab, position, rotation);
            return spawnedObject;
        }
        var objectFromPool = pool.Pop();
        objectFromPool.transform.position = position;
        objectFromPool.transform.rotation = rotation;
        objectFromPool.gameObject.SetActive(true);
        return objectFromPool;
    }

    public void Despawn(TimeKiller timeKillerObject)
    {
        timeKillerObject.gameObject.SetActive(false);
        //if (pool.Count > 500)
        //{
        //    Destroy(timeKillerObject);
        //}
        //else
        //{
            pool.Push(timeKillerObject);
        //}
    }
}
