using TMPro;

public class WalletCurrencyViewer
{
    private WalletService _walletService;
    private TextMeshProUGUI _textMesh;
    private CurrencyType _currencyType;

    public WalletCurrencyViewer(WalletService walletService, TextMeshProUGUI textMesh, CurrencyType currencyType)
    {
        _walletService = walletService;
        _textMesh = textMesh;
        _currencyType = currencyType;

        walletService.CurrencyChanged += OnCurrencyChanged;
    }

    public void Disable()
    {
        _walletService.CurrencyChanged -= OnCurrencyChanged;
    }

    private void OnCurrencyChanged(CurrencyType currencyType, int value)
    {
        if (currencyType == _currencyType)
            _textMesh.SetText($"{value}");
    }
}