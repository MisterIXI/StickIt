using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BalloonColorizer : MonoBehaviour
{
    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
    }
}