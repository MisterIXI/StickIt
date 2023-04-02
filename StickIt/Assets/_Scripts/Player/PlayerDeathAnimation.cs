using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerDeathAnimation : MonoBehaviour
{
    private PlayerController _playerController;
    [field: SerializeField] private SpriteRenderer _spriteRenderer;
    [field: SerializeField] private ParticleSystem _particleSystem;
    [field: SerializeField] private float _deathAnimationDuration = 1f;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeath;
    }
    private void OnPlayerDeath()
    {
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        Color initialColor = _spriteRenderer.color;
        _particleSystem.Play();
        for (int i = 0; i < 30; i++)
        {
            _spriteRenderer.color = Color.Lerp(initialColor, Color.red, i / 30f);
            yield return new WaitForSeconds(_deathAnimationDuration / 30f);
        }
        _spriteRenderer.enabled = false;
    }


}