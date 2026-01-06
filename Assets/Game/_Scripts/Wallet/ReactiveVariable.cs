using System;

public class ReactiveVariable<T>: IReadOnlyVariable<T> where T : IEquatable<T>
{
    public event Action<T> Changed;

    private T _value;

    public ReactiveVariable() => _value = default(T);

    public ReactiveVariable(T value) => _value = value;

    public T Value
    {
        get => _value;

        set
        {
            if (_value.Equals(value) == false)
                Changed?.Invoke(value);

            _value = value;
        }
    }
}