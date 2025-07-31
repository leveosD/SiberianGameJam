using UnityEngine;

public class MainUIController : MonoBehaviour
{
    private RectTransform[] _widgets;

    private void Awake()
    {
        _widgets = GetComponentsInChildren<RectTransform>();
        _widgets[1].gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputController.PlayerFall += OnPlayerFall;
        InputController.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        InputController.PlayerFall -= OnPlayerFall;
        InputController.PlayerDead -= OnPlayerDead;
    }

    private void OnPlayerFall()
    {
        _widgets[4].gameObject.SetActive(false);
        _widgets[1].gameObject.SetActive(true);
    }

    private void OnPlayerDead()
    {
        _widgets[1].gameObject.SetActive(false);
        _widgets[4].gameObject.SetActive(true);
    }
}
