using UnityEngine;

public class SpriteRendererLayerHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private bool runsOnUpdate = false;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sortingOrder = -(int) transform.position.z;
    }

    private void Update()
    {
        if (runsOnUpdate)
        {
            spriteRenderer.sortingOrder = -(int)transform.position.z;
        }
    }
}
