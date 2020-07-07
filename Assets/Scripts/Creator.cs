
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public float minSpawnTime = 1;
    public float maxSpawnTime = 2;

    public float force = 10;

    public int objectCountToSpawn;

    public GameObject[] basicPrefabs;
    public GameObject[] singletonPrefabs;

    private List<GameObject> allPrefabs;

    void Start()
    {
        allPrefabs = basicPrefabs.ToList();
        allPrefabs.AddRange(singletonPrefabs);
    }

    public void SpawnRandom()
    {
        StartCoroutine(SpawnRandomObject());
    }

    private IEnumerator SpawnRandomObject()
    {
        var waitTime = Random.Range(minSpawnTime, maxSpawnTime);

        for (int i = 0; i < objectCountToSpawn; i++)
        {
            var rng = (int)(Random.value * 100) % allPrefabs.Count;
            var prefabToSpawn = allPrefabs[rng].GetComponent<TimeKiller>();
            var spawnedObject = ObjectPool.Instance.Spawn(prefabToSpawn, transform.localPosition, transform.localRotation);
            EjectObject(spawnedObject);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void EjectObject(TimeKiller spawnedObject)
    {
        var rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.velocity = transform.up * force;
        rigidBody.angularVelocity = Random.onUnitSphere * force;
    }
}
