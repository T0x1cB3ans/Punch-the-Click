using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartSong : MonoBehaviour
{
    public Key restartKey = Key.R;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[restartKey].wasPressedThisFrame)
            Restart();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Debug.Log("[RestartSong] Restarting");
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
