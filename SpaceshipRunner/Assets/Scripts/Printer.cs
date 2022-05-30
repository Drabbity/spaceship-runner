using UnityEngine;
using TMPro;

public abstract class Printer : MonoBehaviour
{
    [SerializeField] private string _textBeforeScore;
    private TextMeshProUGUI _tmpro;

    protected virtual void Start()
    {
        _tmpro = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void Update()
    {
        _tmpro.text = _textBeforeScore + GetInformationToDisplay();
    }

    protected abstract string GetInformationToDisplay();
}
