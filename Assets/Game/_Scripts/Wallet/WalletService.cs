using System;
using System.Collections.Generic;
using UnityEngine;

public class WalletService
{
    public event Action<CurrencyType, int> CurrencyChanged;

    private Dictionary<CurrencyType, int> _currencies = new Dictionary<CurrencyType, int>();

    public bool TryChangeAmountOnCurrency(CurrencyType currencyType, int value)
    {
        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        if (_currencies[currencyType] + value < 0)
            return false;

        _currencies[currencyType] += value;

        CurrencyChanged?.Invoke(currencyType, _currencies[currencyType]);

        return true;
    }

    public int GetAmountBy(CurrencyType currencyType)
    {
        if (_currencies.TryGetValue(currencyType, out int amount))
            return amount;

        Debug.LogError("Currency is not founded!");

        return 0;
    }


    private void AddNewCurrencyTypeInWallet(CurrencyType currencyType)
    {
        _currencies.Add(currencyType, 0);
    }
}