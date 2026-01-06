using System;

public interface IReadOnlyVariable <T>
{
    event Action<T> Changed;

    T Value { get; }
}