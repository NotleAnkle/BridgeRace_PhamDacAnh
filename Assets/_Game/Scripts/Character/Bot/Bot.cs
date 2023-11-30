using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    // Start is called before the first frame update

    private Vector3 targetPos;
    private Vector3 endPos;
    private Stage curStage;
    [SerializeField] private List<Brick> bricks = new List<Brick>();

    private IState<Bot> curState;

    public override void OnInit()
    {
        base.OnInit();
        agent.speed = speed;
        agent.enabled = false;

        ChangeState(new IdleState());
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        MoveTo(player.transform.position);
    //    }
    //    CheckDestination();
    //}

    void Update()
    {
        if (curState != null)
        {
            curState.OnExecute(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 1f, 0), Vector3.down * 2f);
    }
    public void ChangeState(IState<Bot> state)
    {
        if (curState != null)
        {
            curState.OnExit(this);
        }

        curState = state;

        if (curState != null)
        {
            curState.OnEnter(this);
        }
    }


    public bool CheckDestination()
    {
        float dis = Vector3.Distance(targetPos, transform.position);
        return (dis < 0.1f);
    }

    public void MoveTo(Vector3 pos)
    {
        ContinueMove();
        targetPos = pos;
        agent.SetDestination(pos);
        //ChangeAnim("run");
    }

    private void GetBrickList()
    {
        bricks = stage.GetColorBrickList(colorType);
    }
    
    public void GetNextBrickPos()
    {
        GetBrickList();
        if(bricks.Count == 0)
        {
            stage.SpawnColorBrick(colorType);
            ChangeState(new IdleState());
            return;
        }

        float min = Vector3.Distance(transform.position, bricks[0].transform.position);
        int index = 0;
        for(int i = 0; i < bricks.Count; i++)
        {
            float dis = Vector3.Distance(transform.position, bricks[i].transform.position);
            if(dis < min)
            {
                min = dis;
                index = i;
            }
        }

        MoveTo(bricks[index].transform.position);
    }

    public void StopMove()
    {
        agent.enabled = false;
    }

    public void ContinueMove()
    {
        agent.enabled = true;
    }

    public void SetEndPos(Vector3 pos)
    {
        endPos = pos;
    }

    public void MoveToEndPos()
    {
        MoveTo(endPos);
    }

}
