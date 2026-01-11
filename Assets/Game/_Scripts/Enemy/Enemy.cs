using UnityEngine;

public class Enemy<TSettings> : MonoBehaviour where TSettings : EnemySettings
{
    [field: SerializeField] public virtual TSettings LocalProperties { get; private set; }

    protected virtual void Kill()
    {
        GameObject.Destroy(gameObject);
    }

    public virtual void Initialize (TSettings property)
    {
        LocalProperties = property;
    }
}