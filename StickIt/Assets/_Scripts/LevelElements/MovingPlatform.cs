using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [field: SerializeField] private Rigidbody2D _platform;
    [field: SerializeField] private Stickie _platformStickie;
    [field: SerializeField] private bool _drawGizmos = false;
    [field: SerializeField] float _movementDuration = 1f;
    [field: SerializeField] bool _onlyPingNoPong = false;
    [field: SerializeField][field: Range(3, 100)] int _gizmoSamplePoints = 10;
    [field: SerializeField][field: Range(0f, 1f)] float _startPoint = 0.5f;
    [field: SerializeField] private bool _randomStartPoint = false;
    [field: SerializeField] private AnimationCurve _movementX;
    [field: SerializeField] private AnimationCurve _movementY;

    private float _animationTime;

    private void Start()
    {
        if (_randomStartPoint)
            _startPoint = Random.value;
        _animationTime = _startPoint * _movementDuration;
    }

    private void FixedUpdate()
    {
        if (!_platformStickie.IsStuck)
        {
            _animationTime += Time.fixedDeltaTime;
            float t = 0f;
            if (_onlyPingNoPong)
                t = (_animationTime % _movementDuration) / _movementDuration;
            else
                t = Mathf.PingPong(_animationTime / _movementDuration, 1f);
            float x = _movementX.Evaluate(t);
            float y = _movementY.Evaluate(t);
            Vector3 newPosition = new Vector3(x, y, 0);
            _platform.MovePosition(newPosition + transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (_drawGizmos)
        {
            Vector3 position = transform.position;
            Vector3 lastPosition = position;
            Gizmos.color = Color.yellow;
            for (int i = 0; i < _gizmoSamplePoints; i++)
            {
                float t = i / (float)(_gizmoSamplePoints - 1);
                float x = _movementX.Evaluate(t);
                float y = _movementY.Evaluate(t);
                Vector3 newPosition = new Vector3(x, y, 0);
                if (i > 0)
                    Gizmos.DrawLine(lastPosition, newPosition + position);
                lastPosition = newPosition + position;
            }
            Gizmos.color = Color.blue;
            float startX = _movementX.Evaluate(_startPoint);
            float startY = _movementY.Evaluate(_startPoint);
            Vector3 startPosition = new Vector3(startX, startY, 0);
            Gizmos.DrawSphere(startPosition + position, 0.1f);
        }
    }
}