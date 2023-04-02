using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectSpawnerPool : MonoBehaviour
{
    private Queue<GameObject>[] _pools;
    private GameObject[] _prefabs;
    public void InitPool(int startCount, GameObject[] prefabs)
    {
        _prefabs = prefabs;
        _pools = new Queue<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            _pools[i] = new Queue<GameObject>();
            for (int j = 0; j < startCount; j++)
            {
                InstantiateNewObject(i);
            }
        }
    }

    private void InstantiateNewObject(int prefabIndex)
    {
        GameObject newObject = Instantiate(_prefabs[prefabIndex], transform);
        newObject.name = "pooled_obj_" + _prefabs[prefabIndex].name + " (clone)";
        newObject.SetActive(false);
        _pools[prefabIndex].Enqueue(newObject);
    }
    public void SpawnObjectAt(Vector3 position, Vector3 velocity, Quaternion rotation, int prefabIndex)
    {
        if (_pools[prefabIndex].Count == 0)
        {
            InstantiateNewObject(prefabIndex);
        }
        GameObject spawnedObject = _pools[prefabIndex].Dequeue();
        spawnedObject.name = "pooled_obj_" + _prefabs[prefabIndex].name + " (clone)";
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        spawnedObject.SetActive(true);
        Rigidbody2D rigidbody2D = spawnedObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.velocity = velocity;
        }
    }

    public void ReturnObject(GameObject returnedObject, int prefabIndex)
    {
        returnedObject.SetActive(false);
        returnedObject.name = "pooled_obj_" + _prefabs[prefabIndex].name + " (clone)";
        _pools[prefabIndex].Enqueue(returnedObject);
    }
}