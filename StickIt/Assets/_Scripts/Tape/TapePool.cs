using System.Collections.Generic;
using UnityEngine;
public class TapePool : MonoBehaviour
{
    public static TapePool Instance { get; private set; }
    [field: SerializeField] public TapeUpdater TapePrefab { get; private set; }
    private TapeSettings _tapeSettings;
    private Queue<TapeUpdater> _tapePool;
    private int OrderNumber = 100;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (TapePrefab is null)
        {
            Debug.LogError("TapePrefab is null, self destructing...");
            Destroy(gameObject);
            return;
        }
        _tapePool = new Queue<TapeUpdater>();
        _tapeSettings = Settingsmanager.Instance.TapeSettings;
        SpawnObjects();
    }

    public static TapeUpdater GetTape()
    {
        if (Instance._tapePool.Count == 0)
            Instance.AddObjectToQueue();
        var tape = Instance._tapePool.Dequeue();
        tape.gameObject.SetActive(true);
        tape.TapeRenderer.sortingOrder = Instance.OrderNumber++;
        return tape;
    }
    public static void ReturnTape(TapeUpdater tape)
    {
        tape.gameObject.SetActive(false);
        Instance._tapePool.Enqueue(tape);
    }


    private void AddObjectToQueue()
    {
        var tape = Instantiate(TapePrefab, transform);
        tape.gameObject.SetActive(false);
        _tapePool.Enqueue(tape);
    }
    private void SpawnObjects()
    {
        for (int i = 0; i < _tapeSettings.TapePoolInitSize; i++)
        {
            AddObjectToQueue();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}