using UnityEngine;

public class EnemyExample : MonoBehaviour, IKillable
{
    [field: SerializeField] public bool IsDead { get; private set; }

    public void Kill()
    {
        GameObject.Destroy(this.gameObject);
    }
}