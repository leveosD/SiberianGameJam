using UnityEngine;

public class FloorSizeChanger : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Vector2 mySize = new Vector2(1.5f, 1);

    public Vector2 MySize
    {
        set
        {
            mySize = value;
            UpdateSize();
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateSize();
    }

    private void OnValidate()
    {
        MySize = mySize;
    }
    
    private void UpdateSize()
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
