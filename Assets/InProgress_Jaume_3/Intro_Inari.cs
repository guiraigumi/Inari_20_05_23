using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro_Inari : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        // Register a callback function to be called when the video finishes playing
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer player)
    {
        // Load the next scene asynchronously
        SceneManager.LoadSceneAsync("Scene_2");
    }
}