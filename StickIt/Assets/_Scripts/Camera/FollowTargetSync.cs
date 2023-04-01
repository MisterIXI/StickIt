using UnityEngine;

public class FollowTargetSync : MonoBehaviour
{
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = PlayerController.Instance.transform;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(_playerTransform.position.x, 0, -10f);
    }
}