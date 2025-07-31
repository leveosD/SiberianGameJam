using UnityEngine;

public class FloorSizeChanger : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    protected Vector2 mySize = new Vector2(1.5f, 1);

    public Vector2 MySize
    {
        set
        {
            mySize = value;
            UpdateSize();
        }
    }

    private void OnValidate()
    {
        UpdateSize();
    }
    
    protected void UpdateSize()
    {
        if (!_boxCollider)
        {
            _boxCollider = GetComponent<BoxCollider>();
        }
        if (!_spriteRenderer)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        _spriteRenderer.size = mySize;
        _boxCollider.size = mySize;
    }
}
