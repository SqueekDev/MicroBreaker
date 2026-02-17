using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    [SerializeField] private float _delayBeforeStartTime;
    [SerializeField] private float _actingTime;
    [SerializeField] private float _delayBeforeStepTime;
    [SerializeField] private float _delayAfterStepTime;
    [SerializeField] private List<Transform> _targets;

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();
        _sequence.SetDelay(_delayBeforeStartTime);

        List<Vector3> targets = new List<Vector3>();

        foreach (var item in _targets)
        {
            Vector3 target = item.position;
            targets.Add(target);
        }

        foreach (var target in targets)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(_delayBeforeStepTime);
            AppendTween(sequence, target);
            sequence.AppendInterval(_delayAfterStepTime);
            _sequence.Append(sequence);
        }

        _sequence.SetLoops(-1, LoopType.Incremental);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause");
            _sequence.Pause();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Play");
            _sequence.Play();
        }
    }

    private void OnDisable()
    {
        _sequence.Kill();
    }

    private void AppendTween(Sequence sequence, Vector3 target)
    {
        sequence.Append(transform.DOMove(target, _actingTime).SetEase(Ease.Linear));
    }
}
