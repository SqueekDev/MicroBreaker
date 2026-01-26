using UnityEngine;

public class TestListener : MonoBehaviour
{
    [SerializeField] private TestNotifier _notifier;
    [SerializeField] private TestEnum _targetBooster;

    private void OnEnable()
    {
        _notifier.Picked += OnBoosterPicked;
    }

    private void OnDisable()
    {
        _notifier.Picked -= OnBoosterPicked;
    }

    private void OnBoosterPicked(TestEnum booster)
    {
        if (booster == _targetBooster)
        {
            Debug.Log("RIGHT!");
        }
        else
        {
            Debug.Log("WRONG");
        }
    }
}
