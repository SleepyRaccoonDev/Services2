public interface IKillable
{
    bool IsDead { get; }

    void Kill();
}
