using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite underPointer;
    private Sprite withoutPointer;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        withoutPointer = _image.sprite;
    }

    private void OnDisable()
    {
        _image.sprite = withoutPointer;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = underPointer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = withoutPointer;
    }
}
