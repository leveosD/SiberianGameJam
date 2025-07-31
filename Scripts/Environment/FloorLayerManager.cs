using UnityEngine;

public class FloorLayerManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("player is come. " + gameObject.name);
            //_spriteRenderer.sortingOrder = -1;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("player is gone. " + gameObject.name);
            //_spriteRenderer.sortingOrder = -2;
        }
    }
}
