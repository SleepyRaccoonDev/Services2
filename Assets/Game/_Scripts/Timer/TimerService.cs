using System;
using System.Collections;
using UnityEngine;

public class TimerService
{
    public event Action TimesUp;

    private MonoBehaviour _mono;

    private ReactiveVariable<float> _startTime = new ReactiveVariable<float>();
    private ReactiveVariable<float> _currentTime = new ReactiveVariable<float>();

    private Coroutine _currentCoroutine;
    private bool _isTimerPaused;

    public TimerService(MonoBehaviour mono)
    {
        _mono = mono;
    }

    public IReadOnlyVariable<float> StartTime => _startTime;
    public IReadOnlyVariable<float> CurrentTime => _currentTime;
    public bool IsUp => _currentTime.Value <= 0;

    public void Start(float value)
    {
        if (value < 0)
            return;

        _isTimerPaused = false;

        if (_currentCoroutine != null)
            _mono.StopCoroutine(_currentCoroutine);

        _startTime.Value = value;

        _currentCoroutine = _mono.StartCoroutine(StartCountdownProcess());
    }

    public void Restart()
    {
        if (_startTime.Value <= 0)
            return;

        Start(_startTime.Value);
    }

    public void TogglePause()
    {
        if (_currentCoroutine == null)
            return;

        _isTimerPaused = !_isTimerPaused;
    }

    private IEnumerator StartCountdownProcess()
    {
        _currentTime.Value = _startTime.Value;

        while (_currentTime.Value > 0)
        {
            yield return new WaitWhile(() => _isTimerPaused);

            _currentTime.Value -= Time.deltaTime;
        }

        _currentTime.Value = 0;

        TimesUp?.Invoke();
    }
}