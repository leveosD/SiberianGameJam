using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private int _currentHealth;
    public int maxHealth;
    
    private Image _fillImage;

    private void Awake()
    {
        WolfSpawner.WolfAwake += Visible;
        InputController.PlayerFall += Invisible;
    }

    void OnEnable()
    {
        _fillImage = GetComponent<Image>();
        _currentHealth = maxHealth;
    }

    private void OnDestroy()
    {
        WolfSpawner.WolfAwake -= Visible;
        InputController.PlayerFall -= Invisible;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        //_currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if(maxHealth == 0)
            return;
        float fillAmount = (float)_currentHealth / maxHealth;
        _fillImage.fillAmount = fillAmount;
    }

    private void Visible()
    {
        _currentHealth = maxHealth;
        UpdateHealthBar();
        _fillImage.enabled = true;
    }

    private void Invisible()
    {
        _fillImage.enabled = false;
    }
}