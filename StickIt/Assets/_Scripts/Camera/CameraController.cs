using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;
    private float _cameraVelocity = 0f;
    private PlayerSettings _playerSettings;

    private void Start()
    {
        _playerSettings = SettingsManager.Instance.PlayerSettings;
        _playerTransform = PlayerController.Instance.transform;
        transform.position = new Vector3(_playerTransform.position.x, 0, -10f);
    }

    private void Update()
    {
        float newX = Mathf.SmoothDamp(transform.position.x, _playerTransform.position.x, ref _cameraVelocity, _playerSettings.CameraFollowTime, _playerSettings.CameraMaxSpeed);
        transform.position = new Vector3(newX, 0, -10f);
    }
}