using TMPro;
using UnityEngine;

public class TimerViewer :MonoBehaviour
{
    private TimerService _timerService;
    private TextMeshProUGUI _textMeshPro;

    public void Initialize(TimerService timerService, TextMeshProUGUI textMeshPro)
    {
        _timerService = timerService;
        _textMeshPro = textMeshPro;

        _timerService.CurrentTime.Changed += OnTimeChanged;
    }

    public void OnDisable() => _timerService.CurrentTime.Changed -= OnTimeChanged;

    private void OnTimeChanged(float value) => _textMeshPro.SetText($"{value:0.00}");
}