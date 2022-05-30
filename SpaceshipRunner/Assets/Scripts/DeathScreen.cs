using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _scoreHolder;

    private void Start()
    {
        GameManager.Instance.GameLost += OpenDeathScreen;
    }

    public void OpenDeathScreen()
    {
        _deathScreen.SetActive(true);
        _scoreHolder.SetActive(false);
        GameManager.Instance.GameLost -= OpenDeathScreen;
    }
}
