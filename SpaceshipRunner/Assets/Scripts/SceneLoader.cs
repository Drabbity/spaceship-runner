using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.SceneChanged();
        SceneManager.LoadScene(sceneName);
    }
}