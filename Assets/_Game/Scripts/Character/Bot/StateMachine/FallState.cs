using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState<Bot>
{
    float timer = 0f;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim("fall");
        for (int i = 0; i < t.playerBrickCount; i++)
        {
            DropBrick dropBrick =  SimplePool.Spawn<DropBrick>(PoolType.DropBrick, t.transform.position + Vector3.up*2f, Quaternion.identity);
            dropBrick.SetStage(t.getStage());
            dropBrick.OnInit();
        }
        t.ClearBrick();
        t.StopMove();
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;

        if(timer > 2f)
        {
            t.ChangeState(new CollectBrickState());
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
