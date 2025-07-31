using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private string property;
    private Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        _text.text = PlayerPrefs.GetString(property);
    }
}
