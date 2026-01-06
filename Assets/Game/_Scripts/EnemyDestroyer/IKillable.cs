public interface IKillable
{
    bool IsDead { get; }
    float LifeTime { get; }

    void Kill();
}
