using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action GameLost;
    private bool _isGameLost = false;
    public int Score { get; private set; }

    public void PlayerDied()
    {
        if (_isGameLost) return;

        GameLost?.Invoke();
        _isGameLost = true;
    }

    public void LoadScene(string sceneName)
    {
        _isGameLost = false;
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateScore(int score)
    {
        Score = score;
    }
}
