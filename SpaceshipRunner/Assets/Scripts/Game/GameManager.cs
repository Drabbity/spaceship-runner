using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action GameLost;
    private bool _isGameLost = false;
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    private void Start()
    {
        Score = 0;
        HighScore = 0;
    }

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

        if (Score > HighScore)
            HighScore = Score;
    }
}
