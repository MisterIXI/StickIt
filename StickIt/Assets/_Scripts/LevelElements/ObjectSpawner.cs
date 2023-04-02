using System.Collections;
using System.Linq;
using UnityEngine;
public class ObjectSpawner : MonoBehaviour
{
    [field: SerializeField] private float ObjectsPerSecond = 1f;
    [field: SerializeField] public bool IsSpawning = true;
    [field: SerializeField] private Vector3 SpawnVelocity = Vector3.zero;
    [field: SerializeField] private bool RandomizeRotation = false;
    [field: SerializeField] private BoxCollider2D SpawnArea;
    [field: SerializeField] private GameObject[] ObjectsToSpawn;
    [field: SerializeField] private ParticleSystem BurnParticlePrefab;
    private ObjectSpawnerPool _objectSpawnerPool;
    private float _timeSinceLastSpawn = 0f;
    private void Awake()
    {
        if (SpawnArea == null)
        {
            Debug.LogError("No spawn area set for ObjectSpawner");
            Destroy(gameObject);
            return;
        }
        if (ObjectsToSpawn == null || ObjectsToSpawn.Length == 0)
        {
            Debug.LogError("No objects to spawn set for ObjectSpawner");
            Destroy(gameObject);
            return;
        }
        _objectSpawnerPool = new GameObject("ObjectSpawnerPool").AddComponent<ObjectSpawnerPool>();
        _objectSpawnerPool.InitPool(10, ObjectsToSpawn);
    }

    private void FixedUpdate()
    {
        if (!IsSpawning) return;
        _timeSinceLastSpawn += Time.fixedDeltaTime;
        if (_timeSinceLastSpawn >= 1f / ObjectsPerSecond)
        {
            SpawnRandomObject();
            _timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnRandomObject()
    {
        int randomObjectIndex = Random.Range(0, ObjectsToSpawn.Length);
        var spawnPosition = new Vector3(Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x), SpawnArea.bounds.center.y, 0f);
        var spawnRotation = RandomizeRotation ? Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) : Quaternion.identity;
        _objectSpawnerPool.SpawnObjectAt(spawnPosition, SpawnVelocity, spawnRotation, randomObjectIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.StartsWith("pooled_obj_") && other.gameObject.name.EndsWith("_spawned"))
        {
            int objectIndex = 0;
            while (objectIndex < ObjectsToSpawn.Length)
            {
                string prefabName = "pooled_obj_" + ObjectsToSpawn[objectIndex].name + " (clone)_spawned";
                if (other.gameObject.name == prefabName)
                {
                    StartCoroutine(DelayedReturn(other.gameObject, objectIndex, 1f));
                    // _objectSpawnerPool.ReturnObject(other.gameObject, objectIndex);
                    // Debug.Log("Returned object " + other.gameObject.name);
                    break;
                }
                objectIndex++;
            }
        }
        // Debug.Log("Trigger enter");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.StartsWith("pooled_obj_"))
        {
            int objectIndex = 0;
            while (objectIndex < ObjectsToSpawn.Length)
            {
                string prefabName = "pooled_obj_" + ObjectsToSpawn[objectIndex].name + " (clone)";
                if (other.gameObject.name == prefabName)
                {
                    // _objectSpawnerPool.ReturnObject(other.gameObject, objectIndex);
                    other.gameObject.name = "pooled_obj_" + ObjectsToSpawn[objectIndex].name + " (clone)_spawned";
                    break;
                }
                objectIndex++;
            }
        }
        // Debug.Log("Trigger exit");
    }

    private IEnumerator DelayedReturn(GameObject obj, int objectIndex, float delay)
    {
        var particles = Instantiate(BurnParticlePrefab, obj.transform);
        yield return new WaitForSeconds(delay);
        _objectSpawnerPool.ReturnObject(obj, objectIndex);
        Destroy(particles.gameObject);
    }
}