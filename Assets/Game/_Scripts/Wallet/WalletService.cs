using System;
using System.Collections.Generic;
using System.Linq;

public class WalletService
{
    private Dictionary<CurrencyType, ReactiveVariable<float>> _currencies = new Dictionary<CurrencyType, ReactiveVariable<float>>();

    public WalletService(CurrencyType currencyType)
    {
        AddNewCurrencyTypeInWallet(currencyType);
    }

    public WalletService()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            AddNewCurrencyTypeInWallet(type);
    }

    public IReadOnlyDictionary<CurrencyType, IReadOnlyVariable<float>> Currencies =>
        _currencies.ToDictionary(
            v => v.Key,
            v => (IReadOnlyVariable<float>)v.Value
        );

    public void Add(CurrencyType currencyType, float value)
    {
        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        if (value <= 0)
        {
            NegativeValueExeption(currencyType);
            return;
        }

        _currencies[currencyType].Value += value;
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
        }

        return false;
    }

    public bool TryGetAmountBy(CurrencyType currencyType, out float result)
    {
        if (_currencies.TryGetValue(currencyType, out ReactiveVariable<float> amount))
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
        throw new Exception($"Currency - {currencyType}. Incoming value is negative!");
    }
}