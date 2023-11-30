using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : ColorObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject brickPackage;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] protected Transform model;

    private string currentAnimName;
    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();

    public int playerBrickCount => playerBricks.Count;

    protected Stage stage;

    protected Vector3 StartPos;

    protected bool IsFall => currentAnimName == "fall";

    private void Awake()
    {
        StartPos = transform.position;
    }

    public virtual void OnInit()
    {
        ChangeAnim(Constant.ANIM_IDLE);
        transform.position = StartPos;
        ClearBrick();
    }
   
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            if (currentAnimName != null)
            {
                anim.ResetTrigger(currentAnimName);
            }

            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public Vector3 CheckGround(Vector3 nextPos)
    {
        RaycastHit hit;

        pos = nextPos;

        if (Physics.Raycast(nextPos + Vector3.up, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point ;
        }
        return transform.position;
    }
    private Vector3 pos;

    public bool CheckStair(Vector3 nextPos)
    {
        bool IsCanMove = true;

        RaycastHit hit;

        if (Physics.Raycast(nextPos + new Vector3(0, 1f, 0), Vector3.down, out hit, 2f, stairLayer))
        {

            StairBrick stairBrick = Cache<StairBrick>.GetScript(hit.collider);

            if (stairBrick.colorType == ColorType.None && model.forward.z < 0f)
            {
                return false;
            }


            if (stairBrick.colorType != colorType && playerBricks.Count > 0)
            {
                RemoveBrick();
                stairBrick.ChangeColor(colorType);
                stage.SpawnOneColorBrick(colorType);
            }

            if (stairBrick.colorType != colorType && playerBricks.Count == 0 && model.forward.z > 0)
            {
                IsCanMove = false;
            }

        }


        return IsCanMove;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BRICK) && !IsFall)
        {

            Brick brick = Cache<Brick>.GetScript(other);

            if (brick.colorType == colorType || brick.colorType == ColorType.Gray)
            {
                AddBrick();
                stage.RemoveBrick(brick);
            }
        }

        else
        {
            if (other.CompareTag(Constant.TAG_DOOR))
            {
                Door door = Cache<Door>.GetScript(other);
                if (door.index != LevelManager.Instance.GetStageIndex(stage))
                {
                    stage.ClearColorBrick(colorType);
                    ChangeStage(LevelManager.Instance.GetStage(door.index));
                    stage.SpawnColorBrick(colorType);
                    door.OnDespawn();
                }

            }
        }
    }

    public void ChangeStage(Stage stage)
    {
        this.stage = stage;
    }

    public Stage getStage()
    {
        return stage;
    }


    public void AddBrick()
    {

        PlayerBrick playerBrick = SimplePool.Spawn<PlayerBrick>(PoolType.PlayerBrick, brickPackage.transform);
        playerBrick.ChangeColor(colorType);

        playerBrick.transform.localPosition = Vector3.up * 0.5f * playerBricks.Count;
        playerBricks.Push(playerBrick);
    }
    public void RemoveBrick()
    {
        SimplePool.Despawn(playerBricks.Pop());
        //Destroy(bricks.Pop().gameObject);
    }

    public void ClearBrick()
    {
        while (playerBricks.Count > 0)
        {
            RemoveBrick();
        }
    }

}
