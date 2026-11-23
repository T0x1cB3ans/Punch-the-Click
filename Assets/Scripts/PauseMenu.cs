using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused {get; private set;}

    public GameObject pausePanel;
    public AudioSource music;
    public string menuSceneName = "Menu";
    public Key pauseKey = Key.Escape;

    void Awake()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[pauseKey].wasPressedThisFrame)
        {
            if (IsPaused) Resume ();
            else Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        music.Pause();
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        music.UnPause();
        pausePanel.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        if (SceneLoader.Instance != null) SceneLoader.Instance.LoadScene(menuSceneName);
        else SceneManager.LoadScene(menuSceneName);
    }
}
