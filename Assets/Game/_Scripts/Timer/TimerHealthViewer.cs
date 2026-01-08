using System;
using UnityEngine;

public class TimerHealthViewer : IDisposable
{
    private TimerService _timerService;

    private Transform _parent;
    private Transform _healthIconPrefub;

    private Factory<Transform> _factory;

    private int _currentValue;

    public TimerHealthViewer(TimerService timerService, Transform parent, Transform healthIconPrefub)
    {
        _timerService = timerService;

        _parent = parent;
        _healthIconPrefub = healthIconPrefub;

        _factory = new Factory<Transform>();

        _timerService.CurrentTime.Changed += OnTimeChanged;
    }

    public void Dispose()
    {
        _timerService.CurrentTime.Changed -= OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        int intValue = Mathf.CeilToInt(value);

        if (intValue == _currentValue)
            return;

        foreach (Transform child in _parent.transform)
            _factory.Return(child);

        for(int i = 0; i < intValue; i++)
            _factory.Get(_healthIconPrefub, Vector2.zero, Quaternion.identity, _parent);

        _currentValue = intValue;
    }
}