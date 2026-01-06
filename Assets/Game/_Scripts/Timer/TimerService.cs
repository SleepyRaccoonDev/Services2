using System;
using System.Collections;
using UnityEngine;

public class TimerService
{
    public event Action<float> TimeChanged;
    public event Action TimesUp;

    private MonoBehaviour _mono;
    private float _timer;
    private float _currentTime;
    private Coroutine _currentCoroutine;

    private bool _isTimerPaused;

    public TimerService(MonoBehaviour mono)
    {
        _mono = mono;
    }

    public float Timer => _timer;

    public void Start(float value)
    {
        if (value < 0)
            return;

        _isTimerPaused = false;

        if (_currentCoroutine != null)
            _mono.StopCoroutine(_currentCoroutine);

        _timer = value;

        _currentCoroutine = _mono.StartCoroutine(StartCountdownProcess());
    }

    public void Restart()
    {
        if (_timer <= 0)
            return;

        Start(_timer);
    }

    public void PauseTimer()
    {
        if (_currentCoroutine == null)
            return;

        _isTimerPaused = !_isTimerPaused;
    }

    private IEnumerator StartCountdownProcess()
    {
        _currentTime = _timer;

        while (_currentTime > 0)
        {
            yield return new WaitWhile(() => _isTimerPaused);

            _currentTime -= Time.deltaTime;

            TimeChanged?.Invoke(_currentTime);
        }

        _currentTime = 0;

        TimesUp?.Invoke();
    }
}