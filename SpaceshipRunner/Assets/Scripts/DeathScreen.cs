using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;

    private void Start()
    {
        GameManager.Instance.GameLost += OpenDeathScreen;
    }

    public void OpenDeathScreen()
    {
        _deathScreen.SetActive(true);
        GameManager.Instance.GameLost -= OpenDeathScreen;
    }
}
