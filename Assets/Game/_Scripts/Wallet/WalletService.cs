using System;
using System.Collections.Generic;
using UnityEngine;

public class WalletService
{
    private Dictionary<CurrencyType, IReadOnlyVariable<float>> _currencies = new Dictionary<CurrencyType, IReadOnlyVariable<float>>();

    public WalletService()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            AddNewCurrencyTypeInWallet(type);
    }

    public IReadOnlyDictionary<CurrencyType, IReadOnlyVariable<float>> Currencies => _currencies;

    public void Add(CurrencyType currencyType, float value)
    {
        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        if (value < 0)
        {
            NegativeValueExeption(currencyType);
            return;
        }  

        if (_currencies[currencyType] is ReactiveVariable<float> ractive)
        {
            ractive.Value += value;
            return;
        }

        ChangeAmountInWalletExeption(currencyType);
    }

    public bool TrySpend(CurrencyType currencyType, float value)
    {
        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        if (value < 0)
        {
            NegativeValueExeption(currencyType);
            return false;
        }

        if (_currencies[currencyType] is ReactiveVariable<float> ractive)
        {
            if (ractive.Value - value >= 0)
            {
                ractive.Value -= value;
                return true;
            }
            else
            {
                NotEnoughMoneyExeption(currencyType);
            }
        }

        ChangeAmountInWalletExeption(currencyType);

        return false;
    }

    public bool TryGetAmountBy(CurrencyType currencyType, out float result)
    {
        if (_currencies.TryGetValue(currencyType, out IReadOnlyVariable<float> amount))
        {
            result = amount.Value;
            return true;
        }

        result = 0;

        return false;
    }


    private void AddNewCurrencyTypeInWallet(CurrencyType currencyType)
    {
        _currencies.Add(currencyType, new ReactiveVariable<float>(default));
    }

    private void NegativeValueExeption(CurrencyType currencyType)
    {
        Debug.Log($"Currency - {currencyType}. Incoming value is negative!");
    }

    private void ChangeAmountInWalletExeption(CurrencyType currencyType)
    {
        Debug.Log($"Currency - {currencyType}. The balance has not been topped up!");
    }

    private void NotEnoughMoneyExeption(CurrencyType currencyType)
    {
        Debug.Log($"Currency - {currencyType}. Not enough money!");
    }
}