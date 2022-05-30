using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float _blinkScaleMultiplier = 1.1f;
    [SerializeField] private float _blinkFrequency = 0.7f;

    private Vector3 _startingScale;

    private bool _isBlinking = false;

    void Start()
    {
        _startingScale = transform.localScale;
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            if(_isBlinking)
            {
                transform.localScale = _startingScale;
            }
            else
            {
                transform.localScale = _startingScale * _blinkScaleMultiplier;
            }

            _isBlinking = !_isBlinking;

            yield return new WaitForSeconds(_blinkFrequency);
        }
    }
}
