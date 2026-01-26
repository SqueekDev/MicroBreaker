using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private TestEnum _type;

    private void Awake()
    {
        int number1 = 0;
        int number2 = 1;
        int number3 = 2;
        int number4 = 3;
        _type = (TestEnum)number1;
        Debug.Log(_type);
        _type = (TestEnum)number2;
        Debug.Log(_type);
        _type = (TestEnum)number3;
        Debug.Log(_type);
        _type = (TestEnum)number4;
        Debug.Log(_type);
    }
}
