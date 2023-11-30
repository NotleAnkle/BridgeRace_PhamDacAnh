using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player player;
    [SerializeField] private Map[] maps = null;
    private Map curMap;
    private int curMapIndex = 0;

    public void OnInit()
    {
        //curMap = maps[curMapIndex];
        //curMap.SetPlayer(player);
        //curMap.OnInit();
        //UIManager.Instance.SetLevel(curMapIndex + 1);
        LoadMap();
    }

    private void LoadMap()
    {
        if(curMap != null)
        {
            Destroy(curMap.gameObject);
        }

        SimplePool.Collect(PoolType.DropBrick);
        SimplePool.Collect(PoolType.Brick);

        curMap = Instantiate(maps[curMapIndex]);
        curMap.SetPlayer(player);
        curMap.OnInit();
        UIManager.Instance.SetLevel(curMapIndex + 1);
    }

    public void NextLevel()
    {
        player.IncreaCoin(50);
        curMapIndex++;
        GameManager.Instance.ContinueGame();
        if (curMapIndex < maps.Length)
        {
            LoadMap();
        }
    }

    public List<ColorType> GetCharacterColor()
    {
        return curMap.GetCharacterColor();
    }

   public int GetStageIndex(Stage stage)
    {
        return curMap.GetStageIndex(stage);
    }

    public Stage GetStage(int index)
    {
        return curMap.GetStage(index);
    }

    public Vector3 GetWinPos()
    {
        return curMap.GetWinPos();
    }
}
