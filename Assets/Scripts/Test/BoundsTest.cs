using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsTest : MonoBehaviour
{
    [SerializeField] private GameObject _center;
    [SerializeField] private GameObject _min;
    [SerializeField] private GameObject _max;

    private Collider _collider;
    private Vector3 _colCenter;
    private Vector3 _colMin;
    private Vector3 _colMax;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _colCenter = _collider.bounds.center;
        _colMin = _collider.bounds.min;
        _colMax = _collider.bounds.max;
    }

    private void Start()
    {
        Debug.Log($"CENTER: {_colCenter}, MIN: {_colMin}, MAX: {_colMax}");
        Debug.Log($"SIZE X = {_collider.bounds.size.x}");
        _center.transform.position = _colCenter;
        _min.transform.position = _colMin;
        _max.transform.position = _colMax;
    }
}
