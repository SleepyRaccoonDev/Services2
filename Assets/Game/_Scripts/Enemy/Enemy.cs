using UnityEngine;

public class Enemy<T> : MonoBehaviour
{
    [field: SerializeField] public virtual T LocalProperties { get; private set; }

    [field: SerializeField] protected CommonProperties CommonProperties { get; private set; }

    protected virtual void Kill()
    {
        GameObject.Destroy(gameObject);
    }

    public virtual void Initialize (T property)
    {
        LocalProperties = property;
    }
}