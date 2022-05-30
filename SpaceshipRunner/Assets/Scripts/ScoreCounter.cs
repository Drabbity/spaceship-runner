using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    float _startXPosition;

    private void Start()
    {
        _startXPosition = transform.position.x;
    }

    private void Update()
    {
        float score = CalculateScore();
        GameManager.Instance.UpdateScore(Mathf.RoundToInt(score));
    }

    private float CalculateScore()
    {
        return transform.position.x - _startXPosition;
    }
}
