using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.DOMove(_target.position, 10f);
    }
}
