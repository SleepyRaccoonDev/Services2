using System;
using TMPro;

public class WalletCurrencyViewer : IDisposable
{
    private WalletService _walletService;
    private TextMeshProUGUI _textMesh;
    private CurrencyType _currencyType;

    public WalletCurrencyViewer(WalletService walletService, TextMeshProUGUI textMesh, CurrencyType currencyType)
    {
        _walletService = walletService;
        _textMesh = textMesh;
        _currencyType = currencyType;

        walletService.Currencies[_currencyType].Changed += OnCurrencyChanged;
    }

    public void Dispose()
    {
        _walletService.Currencies[_currencyType].Changed -= OnCurrencyChanged;
    }

    private void OnCurrencyChanged(float value)
    {
        _textMesh.SetText($"{value}");
    }
}