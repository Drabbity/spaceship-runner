using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action GameLost;
    private bool _isGameLost = false;

    public void PlayerDied()
    {
        if (_isGameLost) return;

        GameLost?.Invoke();
        _isGameLost = true;
    }

    public void SceneChanged()
    {
        _isGameLost = false;
    }
}
