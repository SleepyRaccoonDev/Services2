using System;
using TMPro;

public class TimerViewer :IDisposable
{
    private TimerService _timerService;
    private TextMeshProUGUI _textMeshPro;

    public TimerViewer(TimerService timerService, TextMeshProUGUI textMeshPro)
    {
        _timerService = timerService;
        _textMeshPro = textMeshPro;

        _timerService.CurrentTime.Changed += OnTimeChanged;
    }

    public void Dispose()
    {
        _timerService.CurrentTime.Changed -= OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        _textMeshPro.SetText($"{value:0.00}");
    }
}