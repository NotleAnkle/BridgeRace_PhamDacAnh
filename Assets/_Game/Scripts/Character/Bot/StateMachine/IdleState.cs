using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;
        if(timer > 1f) {
            t.ChangeState(new CollectBrickState());
        }
    }

    public void OnExit(Bot t)
    {
       
    }
}
