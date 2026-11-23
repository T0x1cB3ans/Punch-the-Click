using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
   public static SceneLoader Instance { get; private set; }

   void Awake()
   {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
   }

   public void LoadScene(string sceneName)
   {
        Debug.Log($"[SceneLoader] Attempting to load scene: '{sceneName}'");
        StartCoroutine(LoadAsync(sceneName));
   }

   public void QuitGame()
   {
     Debug.Log("[SceneLoader] Quitting Game");

     #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
     #else
          Application.Quit();
     #endif
   }

   private IEnumerator LoadAsync(string sceneName)
   {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f) yield return null;

        op.allowSceneActivation = true;
   }
}
