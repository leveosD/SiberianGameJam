using UnityEngine;

public class Floor3DSizeChanger : FloorSizeChanger
{
    private BoxCollider _childCollider;
    private SpriteRenderer _childSprite;
    private Transform _cubeTransform;
    
    [SerializeField]
    protected Vector3 mySize3D = new Vector3(1.5f, 1, 1f);
    
    private void OnValidate()
    {
        UpdateSize();
    }
    
    protected void UpdateSize()
    {
        mySize = mySize3D;
        
        base.UpdateSize();

        if (_childCollider == null)
        {
            _childCollider = GetComponentsInChildren<BoxCollider>()[1];
        }
        if (_childSprite == null)
        {
            _childSprite = GetComponentsInChildren<SpriteRenderer>()[1];
        }
        if (_cubeTransform == null)
        {
            _cubeTransform = GetComponentsInChildren<Transform>()[1];
        }
        
        _childCollider.size = mySize;
        _childSprite.size = mySize;
        _cubeTransform.localScale = mySize3D;
    }
}
