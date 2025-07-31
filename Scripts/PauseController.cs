using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private Slider[] _slider;
    [SerializeField] private AudioMixer main;
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer sounds;
    
    [SerializeField] private AudioMixerSnapshot basic;
    [SerializeField] private AudioMixerSnapshot pause;
    
    void Start()
    {
        _slider = GetComponentsInChildren<Slider>();
        _slider[0].onValueChanged.AddListener(OnSensitivitySliderValueChanged);
        _slider[1].onValueChanged.AddListener(OnMasterVolumeSliderValueChanged);
        _slider[2].onValueChanged.AddListener(OnMusicVolumeSliderValueChanged);
        _slider[3].onValueChanged.AddListener(OnSoundsVolumeSliderValueChanged);

        float sensitivity = PlayerPrefs.GetFloat("SensitivityValue");
        _slider[0].value = sensitivity;

        float volume;
        if (main.GetFloat("Volume", out volume))
        {
            _slider[1].value = volume;
        }
        else
        {
            _slider[1].value = -20f;
        }
        if (music.GetFloat("Volume", out volume))
        {
            _slider[2].value = volume;
        }
        else
        {
            _slider[2].value = -20f;
        }
        if (sounds.GetFloat("Volume", out volume))
        {
            _slider[3].value = volume;
        }
        else
        {
            _slider[3].value = -20f;
        }
    }

    private void OnEnable()
    {
        pause.TransitionTo(0.5f);
    }

    private void OnDisable()
    {
        basic.TransitionTo(0.5f);
    }

    void OnMasterVolumeSliderValueChanged(float value)
    {
        if (value <= -60) value = -80;
        //PlayerPrefs.SetFloat("Master", value);
        main.SetFloat("Volume", value);
    }

    void OnMusicVolumeSliderValueChanged(float value)
    {
        if (value <= -60) value = -80;
        //PlayerPrefs.SetFloat("Music", value);
        music.SetFloat("Volume", value);
    }

    void OnSoundsVolumeSliderValueChanged(float value)
    {
        if (value <= -60) value = -80;
        sounds.SetFloat("Volume", value);
    }

    void OnSensitivitySliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("SensitivityValue", value);
    }
}
