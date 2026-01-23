using UnityEngine;

public class Ork : Enemy
{
    private OrkProperties _orkProperties;

    public void Initialize(OrkProperties orkProperties)
    {
        _orkProperties = orkProperties;
    }
}