using System;
using System.Collections.Generic;
using System.Linq;

public class WalletService
{
    private Dictionary<CurrencyType, ReactiveVariable<int>> _currencies = new Dictionary<CurrencyType, ReactiveVariable<int>>();

    public WalletService(Dictionary<CurrencyType, int> startValues)
    {
        foreach (var value in startValues)
            _currencies.Add(value.Key, new ReactiveVariable<int> (value.Value));
    }

    public WalletService(CurrencyType currencyType)
    {
        AddNewCurrencyTypeInWallet(currencyType);
    }

    public WalletService()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            AddNewCurrencyTypeInWallet(type);
    }

    public IReadOnlyDictionary<CurrencyType, IReadOnlyVariable<int>> Currencies =>
        _currencies.ToDictionary(
            v => v.Key,
            v => (IReadOnlyVariable<int>)v.Value
        );

    public void Add(CurrencyType currencyType, int value)
    {
        if (value <= 0)
            NegativeValueExeption(currencyType);

        if (_currencies.ContainsKey(currencyType) == false)
            AddNewCurrencyTypeInWallet(currencyType);

        _currencies[currencyType].Value += value;
    }

    public bool TrySpend(CurrencyType currencyType, int value)
    {
        if (value < 0)
            NegativeValueExeption(currencyType);

        if (_currencies[currencyType] is ReactiveVariable<int> ractive)
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
        if (_currencies.TryGetValue(currencyType, out ReactiveVariable<int> amount))
        {
            result = amount.Value;
            return true;
        }

        result = 0;

        return false;
    }


    private void AddNewCurrencyTypeInWallet(CurrencyType currencyType)
    {
        _currencies.Add(currencyType, new ReactiveVariable<int>(default));
    }

    private void NegativeValueExeption(CurrencyType currencyType)
    {
        throw new Exception($"Currency - {currencyType}. Incoming value is negative!");
    }
}