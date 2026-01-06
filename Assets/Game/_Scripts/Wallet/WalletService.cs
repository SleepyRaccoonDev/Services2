using System;
using System.Collections.Generic;
using UnityEngine;

public class WalletService
{
    private Dictionary<CurrencyType, IReadOnlyVariable<float>> _currencies = new Dictionary<CurrencyType, IReadOnlyVariable<float>>();

    public WalletService()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            TryChangeAmountOnCurrency(type, default);
    }

    public IReadOnlyDictionary<CurrencyType, IReadOnlyVariable<float>> Currencies => _currencies;

    public bool TryChangeAmountOnCurrency(CurrencyType currencyType, float value)
    {
        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        if (_currencies[currencyType].Value + value < 0)
            return ExeptionLogic(currencyType);

        if (_currencies[currencyType] is ReactiveVariable<float> ractive)
        {
            ractive.Value += value;
            return true;
        }

        return ExeptionLogic(currencyType);
    }

    public float GetAmountBy(CurrencyType currencyType)
    {
        if (_currencies.TryGetValue(currencyType, out IReadOnlyVariable<float> amount))
            return amount.Value;

        Debug.LogError("Currency is not founded!");

        return 0;
    }


    private void AddNewCurrencyTypeInWallet(CurrencyType currencyType)
    {
        _currencies.Add(currencyType, new ReactiveVariable<float>(0));
    }

    private bool ExeptionLogic(CurrencyType currencyType)
    {
        Debug.Log($"{currencyType} if wallet is not changed!");
        return false;
    }
}