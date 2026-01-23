using UnityEngine;

public class Dragon : Enemy
{
    private DragonProperties _dragonProperties;

    public void Initialize(DragonProperties dragonProperties)
    {
        _dragonProperties = dragonProperties;
    }
}