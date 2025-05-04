
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartGame : MonoBehaviour
{
    private Button button;
    public VideoPlayer videoPlayer;
    [SerializeField] private Image im1;
    [SerializeField] private Image im2;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void OnButtonClick()
    {
        videoPlayer.Play();
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        im1.enabled = false;
        im1.enabled = false;
        button.enabled = false;
        button.image.enabled = false;
        yield return new WaitForSecondsRealtime(10.5f);
        SceneManager.LoadScene(1);
        
    }
}
