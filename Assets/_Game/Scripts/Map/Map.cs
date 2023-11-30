using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Stage> stages;
    [SerializeField] private List<Bot> bots;
    [SerializeField] private Player player;
    [SerializeField] private Transform winPos;
    [SerializeField] private Transform[] endPos;

    [SerializeField] private Transform playerStartPos;

    private int endPosIndex;

    public void OnInit()
    {

        for(int i = 0; i < bots.Count; i++)
        {
            bots[i].OnInit();
            bots[i].ChangeStage(stages[0]);
            bots[i].SetEndPos(GetNextEndPos());
        }

        if(player != null)
        {
            player.ChangeStage(stages[0]);
            player.OnInit();
            player.transform.position = playerStartPos.position;
        }

        for(int i = 0; i < stages.Count; i++)
        {
            stages[i].OnInit();
        }

        stages[0].SpawnBricks();
    }

    public List<ColorType> GetCharacterColor()
    {
        List<ColorType> colors = new List<ColorType>();
        for(int  i = 0; i < bots.Count; i++)
        {
            colors.Add(bots[i].colorType);
        }
        colors.Add(player.colorType);

        return colors;
    }

    public Stage GetStage(int index)
    {
        return stages[index];
    }

    public int GetStageIndex(Stage stage) { 
        return stages.IndexOf(stage);
    }

    public Vector3 GetWinPos()
    {
        return winPos.position;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public Vector3 GetNextEndPos()
    {
        Vector3 ans = endPos[endPosIndex].position;
        endPosIndex = (endPosIndex + 1) % endPos.Length;
        return ans;
    }
}
