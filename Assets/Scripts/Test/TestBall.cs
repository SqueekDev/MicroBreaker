using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestBall : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.velocity = Vector3.forward * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflect = Vector3.Reflect(-collision.relativeVelocity.normalized, normal);
        _rb.velocity = reflect * _speed;
    }
}
