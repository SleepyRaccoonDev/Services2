using UnityEngine.UI;

public class TimerSliderViewer 
{
    private TimerService _timerService;
    private Slider _slider;

    public TimerSliderViewer(TimerService timerService, Slider slider)
    {
        _timerService = timerService;
        _slider = slider;

        _timerService.TimeChanged += OnTimeChanged;
    }

    public void Disable()
    {
        _timerService.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        _slider.value = value / _timerService.Timer;
    }
}
