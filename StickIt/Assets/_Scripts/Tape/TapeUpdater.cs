using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class TapeUpdater : MonoBehaviour
{
    public bool IsStuck { get; private set; }
    public SpriteRenderer TapeRenderer { get; private set; }
    private BoxCollider2D _collider;
    private HashSet<Stickie> _stickies;
    private HashSet<Stickie> _highlightedStickies;

    private void Awake()
    {
        TapeRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _stickies = new HashSet<Stickie>();
        _highlightedStickies = new HashSet<Stickie>();
    }

    public void UpdateTape(Vector2 startPos, Vector2 endPos)
    {
        // get midpoint between mouse and tape start pos
        Vector2 midPoint = (endPos + startPos) / 2f;
        TapeRenderer.transform.position = midPoint;
        TapeRenderer.transform.right = endPos - startPos;
        // get distance between mouse and tape start pos
        float distance = Vector2.Distance(endPos, startPos);
        // set currentTape tile width to distance
        TapeRenderer.size = new Vector2(distance, TapeRenderer.size.y);
        // set collider size to distance
        _collider.size = new Vector2(distance, _collider.size.y);
        // get all stickies in collider
        var stickies = Physics2D.OverlapBoxAll(_collider.bounds.center, _collider.size, transform.eulerAngles.z);
        // filter colliders that don't have a stickie component
        var stickieComponents = stickies.Select(c => c.GetComponent<Stickie>()).Where(c => c != null).ToHashSet();
        // unhighlight stickies that are no longer in collider
        foreach (var stickie in _highlightedStickies)
        {
            if (!stickieComponents.Contains(stickie))
            {
                stickie.UnHighlight();
            }
        }
        // highlight stickies that are new in collider
        foreach (var stickie in stickieComponents)
        {
            if (!_highlightedStickies.Contains(stickie))
            {
                stickie.Highlight();
            }
        }
        _highlightedStickies = stickieComponents;
    }

    public void CleanUp()
    {
        if (_stickies != null)
        {
            foreach (var stickie in _stickies)
            {
                if (stickie != null)
                {
                    if (IsStuck)
                        stickie.UnStick();
                    else
                        stickie.UnHighlight();
                }
            }
        }
        IsStuck = false;
    }
    public void StickIt()
    {
        foreach (var stickie in _stickies)
        {
            stickie.Stick();
            stickie.UnHighlight();
        }
        IsStuck = true;
    }

    public void CutIt()
    {
        CleanUp();
        _stickies.Clear();
        TapePool.ReturnTape(this);
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void OnEnable()
    {
        // set tapeMat diffuse y offset to 0.5
        TapeRenderer.material.SetTextureOffset("_MainTex", new Vector2(0f, Random.Range(0f, 0.73f)));
    }
}
