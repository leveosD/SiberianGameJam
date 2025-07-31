using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "MyAssets/Language")]
public class Language : ScriptableObject
{
    [SerializeField] private string langName;
    [SerializeField] private string start;
    [SerializeField] private string settings;
    [SerializeField] private string quit;
    [SerializeField] private string menu;
    [SerializeField] private string sensitivity;
    [SerializeField] private string masterVolume;
    [SerializeField] private string music;
    [SerializeField] private string sounds;
    [SerializeField] private string health;
    [SerializeField] private string armor;
    [SerializeField] private string press;
    [SerializeField] private string dead;

    public string LangName => langName;
    public string Start => start;
    public string Settings => settings;
    public string Quit => quit;
    public string Menu => menu;
    public string Sensitivity => sensitivity;
    public string MasterVolume => masterVolume;
    public string Music => music;
    public string Sounds => sounds;
    public string Health => health;
    public string Armor => armor;
    public string Press => press;
    public string Dead => dead;
}
