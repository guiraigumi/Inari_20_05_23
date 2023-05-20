using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvasControlls;
    [SerializeField] private GameObject controllsText;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject frame;
    [SerializeField] private GameObject pauseTittle;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject controllsButton;
    [SerializeField] private GameObject exitButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        canvas.SetActive(false);
    }

    public void ShowControlls()
    {
        Time.timeScale = 0f;
        canvasControlls.SetActive(true);
        resumeButton.SetActive(false);
        controllsButton.SetActive(false);
        exitButton.SetActive(false);
        pauseTittle.SetActive(false);
        background.SetActive(false);
        frame.SetActive(false);
        controllsText.SetActive(true);
    }

    public void DisableControlls()
    {
        Time.timeScale = 0f;
        canvasControlls.SetActive(false);
        resumeButton.SetActive(true);
        controllsButton.SetActive(true);
        exitButton.SetActive(true);
        pauseTittle.SetActive(true);
        background.SetActive(true);
        frame.SetActive(true);
        controllsText.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        canvas.SetActive(false);
        StartCoroutine(LoadMainMenuAsync());
    }

    private IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_Menu");
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}