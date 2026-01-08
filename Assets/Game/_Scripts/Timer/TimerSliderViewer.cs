using System;
using UnityEngine.UI;

public class TimerSliderViewer : IDisposable
{
    private TimerService _timerService;
    private Slider _slider;

    public TimerSliderViewer(TimerService timerService, Slider slider)
    {
        _timerService = timerService;
        _slider = slider;

        _timerService.CurrentTime.Changed += OnTimeChanged;
    }

    public void Dispose()
    {
        _timerService.CurrentTime.Changed -= OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        _slider.value = value / _timerService.StartTime.Value;
    }
}
