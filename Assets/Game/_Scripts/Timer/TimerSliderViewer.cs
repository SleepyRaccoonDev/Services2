using UnityEngine;
using UnityEngine.UI;

public class TimerSliderViewer : MonoBehaviour
{
    private TimerService _timerService;
    private Slider _slider;

    public void Initialize(TimerService timerService, Slider slider)
    {
        _timerService = timerService;
        _slider = slider;

        _timerService.CurrentTime.Changed += OnTimeChanged;
    }

    public void OnDisable() => _timerService.CurrentTime.Changed -= OnTimeChanged;

    private void OnTimeChanged(float value) => _slider.value = value / _timerService.StartTime.Value;
}
