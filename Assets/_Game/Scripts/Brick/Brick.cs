using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
