using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvokerOne : MonoBehaviour
{
    [SerializeField] private TestEnum _number;

    public TestEnum Booster => _number;

    private void Awake()
    {
        _number = (TestEnum)4;
    }
}
