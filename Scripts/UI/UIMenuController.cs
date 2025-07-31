using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIMenuController : MonoBehaviour
{
    private Button button;
    public VideoPlayer videoPlayer;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private AudioSource music;

    [SerializeField] private Language language;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        button = GetComponent<Button>();

        if (!PlayerPrefs.HasKey("Start"))
        {
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
        }

        PlayerPrefs.DeleteKey("HealthValue");
        PlayerPrefs.DeleteKey("ArmorValue");
        PlayerPrefs.SetInt("HealthValue", 5);
        PlayerPrefs.SetInt("ArmorValue", 0);
    }

    public void OnStartButtonClicked()
    {
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        Destroy(canvas);
        music.enabled = false;
        videoPlayer.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene(1);

    }

    public void OnSettingsButtunClicked()
    {
        canvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void OnMenusButtunClicked()
    {
        canvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
