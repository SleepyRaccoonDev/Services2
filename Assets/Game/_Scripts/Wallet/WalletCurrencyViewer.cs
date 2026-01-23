using TMPro;
using UnityEngine;

public class WalletCurrencyViewer : MonoBehaviour
{
    private WalletService _walletService;
    private TextMeshProUGUI _textMesh;
    private CurrencyType _currencyType;

    public void Initialize(WalletService walletService, TextMeshProUGUI textMesh, CurrencyType currencyType)
    {
        _walletService = walletService;
        _textMesh = textMesh;
        _currencyType = currencyType;

        walletService.Currencies[_currencyType].Changed += OnCurrencyChanged;
    }

    private void OnDisable() => _walletService.Currencies[_currencyType].Changed -= OnCurrencyChanged;

    private void OnCurrencyChanged(int value) => _textMesh.SetText($"{value}");
}