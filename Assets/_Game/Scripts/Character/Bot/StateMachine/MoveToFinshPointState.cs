using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFinshPointState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.MoveToEndPos();
    }

    public void OnExecute(Bot t)
    {
        if(t.playerBrickCount < 1)
        {
            t.StopMove();
            t.ChangeState(new CollectBrickState());
        }
        else
        {
            t.CheckStair(t.TF.position);
        }
    }

    public void OnExit(Bot t)
    {
    }
}
