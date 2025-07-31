using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PickedItem : MonoBehaviour, IPickable
{
    [SerializeField] protected ScriptableObject item;
    protected AudioSource audioSource;
    protected bool picked;
    private float _startY;

    private void Awake()
    {
        _startY = transform.position.y;
        audioSource = GetComponent<AudioSource>();
        
        transform.DOMoveY(_startY + 0.05f, 0.75f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !picked)
        {
            picked = true;
            audioSource.Play();
            PickItem(other.gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            LaterEffect();
        }
    }

    protected virtual void LaterEffect()
    {
        
    }

    public virtual void PickItem(GameObject go)
    {
        
    }
}
