using UnityEngine;

public class OvenChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer _image;

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InputController.GamePause += CheckLanguage;
        
        SetLanguage();
    }

    private void OnDisable()
    {
        InputController.GamePause -= CheckLanguage;
    }

    private void CheckLanguage(bool state)
    {
        if (!state)
        {
            SetLanguage();
        }
    }

    private void SetLanguage()
    {
        string lang = PlayerPrefs.GetString("LangName");
        if (lang == "Rus")
        {
            _image.sprite = sprites[1];
        }
        else
        {
            _image.sprite = sprites[0];
        }
    }
}
