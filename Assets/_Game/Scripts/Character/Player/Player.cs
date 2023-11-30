using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    


    [SerializeField] private float speed = 5f;

    private int coin = 0;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        UIManager.Instance.SetCoin(coin);
    }


    // Update is called once per frame
    void Update()
    {
        if (!IsFall)
        {
            if (joystick.Direction != Vector2.zero)
            {
                Run();
            }
            else
            {
                Idle();
            }
        }
    }

    private void Run()
    {
        Vector2 deltaPos = joystick.Direction * speed * Time.deltaTime;
        Vector3 nextPos = new Vector3 (deltaPos.x, 0f, deltaPos.y) + transform.position;
        
        if (CheckStair(nextPos))
        {
            transform.position = CheckGround(nextPos);
        }
        ChangeAnim("run");

        Turn();
    }

    private void Idle()
    {
        ChangeAnim("idle");
    }

    private void Turn()
    {
       model.transform.forward = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);
    }

    public void Fall()
    {
        DropBrick dropBrick = SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position + Vector3.up * 2f, Quaternion.identity);
        dropBrick.SetStage(this.getStage());
        dropBrick.OnInit();

        ClearBrick();
        ChangeAnim("fall");
    }

    public void StandUp()
    {
        Idle();
    }

    public void IncreaCoin(int plus)
    {
        coin += plus;
        UIManager.Instance.SetCoin(coin);
    }
}
