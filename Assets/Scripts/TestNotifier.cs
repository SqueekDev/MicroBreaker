using System;
using System.Collections.Generic;
using UnityEngine;

public class TestNotifier : MonoBehaviour
{
    public Action<TestEnum> Picked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TestInvokerOne invoker))
        {
            Debug.Log(invoker.Booster);
            Picked?.Invoke(invoker.Booster);
        }
    }
}
