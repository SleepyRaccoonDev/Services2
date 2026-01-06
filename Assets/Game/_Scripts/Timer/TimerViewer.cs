using System;
using TMPro;

public class TimerViewer
{
    private TimerService _timerService;
    private TextMeshProUGUI _textMeshPro;

    public TimerViewer(TimerService timerService, TextMeshProUGUI textMeshPro)
    {
        _timerService = timerService;
        _textMeshPro = textMeshPro;

        _timerService.TimeChanged += OnTimeChanged;
    }

    public void Disable()
    {
        _timerService.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        _textMeshPro.SetText($"{value:0.00}");
    }
}