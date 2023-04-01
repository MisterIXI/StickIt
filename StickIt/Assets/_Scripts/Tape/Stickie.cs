using UnityEngine;

public class Stickie : MonoBehaviour
{
    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _constraints;
    private SpriteRenderer _spriteRenderer;
    private int stickCount = 0;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _constraints = _rb.constraints;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Stick()
    {
        stickCount++;
        // Debug.Log($"Stickie.Stick() {stickCount}");
        if (stickCount == 1)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnStick()
    {
        stickCount--;
        // Debug.Log($"Stickie.UnStick() {stickCount}");
        if (stickCount == 0)
        {
            _rb.constraints = _constraints;
            _rb.WakeUp();
        }
    }

    public void Highlight()
    {
        // activate outline
        _spriteRenderer.material.SetFloat("_Outline", 0.1f);
        _spriteRenderer.material.color = Color.red;
    }

    public void UnHighlight()
    {
        // deactivate outline
        _spriteRenderer.material.SetFloat("_Outline", 0f);
        _spriteRenderer.material.color = Color.white;
    }
}