using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestTween : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        int firstInt = 1;
        int secontInt = 30;
        int hundred = 100;
        int percentage = firstInt * hundred / secontInt;
        Debug.Log(percentage);
    }

    private void Start()
    {
        _rigidbody.DOMoveX(1f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        _rigidbody.DOMoveZ(20f, 3f);
    }
}
