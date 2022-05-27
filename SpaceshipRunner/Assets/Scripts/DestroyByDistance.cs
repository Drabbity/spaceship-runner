using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    [SerializeField] private string _targetTag;
    [SerializeField] private float _destroyDistance;

    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(_targetTag);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - _target.transform.position.x) > _destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
