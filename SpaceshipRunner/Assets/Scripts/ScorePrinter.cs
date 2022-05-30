using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePrinter : MonoBehaviour
{
    [SerializeField] private string _textBeforeScore;
    private TextMeshProUGUI _tmpro;

    void Start()
    {
        _tmpro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _tmpro.text = _textBeforeScore + GameManager.Instance.Score.ToString();
    }
}
