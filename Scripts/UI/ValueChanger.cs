using UnityEngine;
using UnityEngine.UI;

public class ValueChanger : MonoBehaviour
{
    private Text _label;

    private string _healthStr;
    private string _armorStr;

    private int _health = 5;
    private int _armor = 0;

    private void OnEnable()
    {
        if (!_label)
            _label = GetComponentInChildren<Text>();
        
        _healthStr = PlayerPrefs.GetString("Health");
        _armorStr = PlayerPrefs.GetString("Armor");
        
        ChangeStats(_health, _armor);
        
        InputController.OnStatsChanged += ChangeStats;

    }

    private void OnDisable()
    {
        InputController.OnStatsChanged -= ChangeStats;
    }

    private void ChangeStats(int health, int armor)
    {
        _label.text = _healthStr + ": " + health + "\n" + _armorStr + ": " + armor;
        _health = health;
        _armor = armor;
    }
}
