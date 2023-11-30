using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int brickNumber = 49;

    [SerializeField] private List<Vector3> emptyPos = new List<Vector3>();
    [SerializeField] private List<Brick> bricks = new List<Brick>();

    private int playerAmount = 2;
    private List<ColorType> colorList = new List<ColorType>();
    int amount;

    //private void Start()
    //{
    //    OnInit();
    //}

    private void Awake()
    {
        GetEmptyPos();
    }

    public void OnInit()
    {
        ClearBrick();

        colorList = LevelManager.Instance.GetCharacterColor();

        playerAmount = colorList.Count;

        amount = brickNumber / playerAmount;
    }

    private void ClearBrick()
    {
        for (int i = 0; i < colorList.Count; i++){
            ClearColorBrick(colorList[i]);
        }
    }

    private void GetEmptyPos()
    {
        float num = Mathf.Sqrt(brickNumber);
        float spaceX = transform.lossyScale.x / num;
        float spaceZ = transform.lossyScale.z / num;

        float y = 0.5f;

        Vector3 basePosition = transform.position + new Vector3(0, y, 0);
        emptyPos.Add(basePosition);

        for (int i = 1; i <= num / 2; i++)
        {
            Vector3 offset = new Vector3(i * spaceX, 0, 0);

            emptyPos.Add(basePosition + offset);
            emptyPos.Add(basePosition - offset);

            offset = new Vector3(0, 0, i * spaceZ);

            emptyPos.Add(basePosition + offset);
            emptyPos.Add(basePosition - offset);

            for (int j = 1; j <= num / 2; j++)
            {
                offset = new Vector3(i * spaceX, 0, j * spaceZ);

                emptyPos.Add(basePosition + offset);
                emptyPos.Add(basePosition - offset);

                offset = new Vector3(-i * spaceX, 0, j * spaceZ);

                emptyPos.Add(basePosition + offset);
                emptyPos.Add(basePosition - offset);
            }
        }
    }

    public void SpawnBricks()
    {
        for(int i = 0; i < colorList.Count; i++)
        {
            SpawnColorBrick(colorList[i]);
        }
    }

    public void SpawnColorBrick(ColorType colorType)
    {

        for(int i = 0; i <=  amount; i++)
        {
            SpawnOneColorBrick(colorType);
        }
    }

    public void SpawnOneColorBrick(ColorType colorType)
    {
        if (emptyPos.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, emptyPos.Count);

            Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick, emptyPos[rand], Quaternion.identity);
            //brick.transform.SetParent(platform.transform);
            brick.ChangeColor(colorType);

            emptyPos.RemoveAt(rand);
            bricks.Add(brick);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        brick.OnDespawn();
        
        bricks.Remove(brick);
        if(brick.colorType != ColorType.Gray)
        {
            emptyPos.Add(brick.transform.position);
        }
    }

    public List<Brick> GetColorBrickList(ColorType colorType)
    {
        List<Brick> list = new List<Brick>();

        for(int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                list.Add(bricks[i]);
            }
        }

        return list;
    }

    public void ClearColorBrick(ColorType colorType)
    {
        Stack<Brick> removeBricks = new Stack<Brick> ();
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                removeBricks.Push(bricks[i]);
            }
        }
        while(removeBricks.Count > 0)
        {
            RemoveBrick(removeBricks.Pop());
        }
    }

    public void SpawnGrayBrickAt(Transform transform)
    {
        Brick brick = SimplePool.Spawn<Brick>(PoolType.Brick, transform.position, transform.rotation);
        brick.ChangeColor(ColorType.Gray);

        bricks.Add(brick);
        colorList.Add(ColorType.Gray);
    }
}
