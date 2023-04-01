using UnityEngine;

public class Stickie : MonoBehaviour
{
    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _constraints;
    private int stickCount = 0;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _constraints = _rb.constraints;
    }

    public void Stick()
    {
        stickCount++;
        if (stickCount == 1)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnStick()
    {
        stickCount--;
        if (stickCount == 0)
        {
            _rb.constraints = _constraints;
        }
    }

    public void Highlight()
    {
        // activate outline
        
    }

    public void UnHighlight()
    {

    }
}