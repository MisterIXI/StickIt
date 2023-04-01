using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TapeCutter : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private Vector2 _lastPosition;
    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("TapeCutter OnTriggerEnter2D");
        // if (other.TryGetComponent(out TapeUpdater tape))
        // {
        //     tape.CutIt();
        // }
    }

    public void ResetPosition(Vector2 position)
    {
        _lastPosition = position;
        transform.position = position;
    }

    public void CutToPosition(Vector2 position)
    {
        Vector2 direction = position - _lastPosition;
        Vector2 midPoint = (position + _lastPosition) / 2f;
        float cutAngle = Vector2.SignedAngle(Vector2.right, direction);
        transform.position = position;
        _lastPosition = position;
        var colliders = Physics2D.OverlapBoxAll(midPoint, new Vector2(direction.magnitude, 0.5f), cutAngle);
        foreach (var collider in colliders)
        {
            // Debug.Log($"Collider: {collider.name}");
            if (collider.TryGetComponent(out TapeUpdater tape))
            {
                tape.CutIt();
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedTrailClear());
    }

    private IEnumerator DelayedTrailClear()
    {
        // wait for physics step for position to be updated
        yield return new WaitForFixedUpdate();
        _trailRenderer.Clear();
        _lastPosition = transform.position;
    }
}