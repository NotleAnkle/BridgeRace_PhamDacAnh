using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBrickState : IState<Bot>
{
    private float limit;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_RUN);
        t.GetNextBrickPos();
        t.ContinueMove();
        limit = Random.Range(3, 9);
    }

    public void OnExecute(Bot t)
    {
        if (t.CheckDestination())
        {
            t.GetNextBrickPos();
        }
        if(t.playerBrickCount >= limit)
        {
            t.ChangeState(new MoveToFinshPointState());
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
