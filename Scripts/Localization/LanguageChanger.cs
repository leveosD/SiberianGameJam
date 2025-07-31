using UnityEngine;
using UnityEngine.UI;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private Language language;
    
    private Button _button;
    [SerializeField] private Button anotherButton;
    
    [SerializeField] private Sprite yellowButton;
    [SerializeField] private Sprite greenButton;

    [SerializeField] private string[] keys;
    [SerializeField] private Text[] texts;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _button.image.sprite = yellowButton;
        anotherButton.image.sprite = greenButton;
        
        PlayerPrefs.SetString("LangName", language.LangName);
        PlayerPrefs.SetString("Start", language.Start);
        PlayerPrefs.SetString("Settings", language.Settings);
        PlayerPrefs.SetString("Quit", language.Quit);
        PlayerPrefs.SetString("Menu", language.Menu);
        PlayerPrefs.SetString("Sensitivity", language.Sensitivity);
        PlayerPrefs.SetString("MasterVolume", language.MasterVolume);
        PlayerPrefs.SetString("Music", language.Music);
        PlayerPrefs.SetString("Sounds", language.Sounds);
        PlayerPrefs.SetString("Health", language.Health);
        PlayerPrefs.SetString("Armor", language.Armor);
        PlayerPrefs.SetString("Press", language.Press);
        PlayerPrefs.SetString("Dead", language.Dead);

        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = PlayerPrefs.GetString(keys[i]);
        }
    }
}
