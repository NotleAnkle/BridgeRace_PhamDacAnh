using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject joystick;

    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start()
    {
        UIManager.Instance.OpenPanel(UIPanel.MainMenu);
        LevelManager.Instance.OnInit();
        PauseGame();
    }

    public void OnWin()
    {
        UIManager.Instance.OpenPanel(UIPanel.WinUI);
        PauseGame();
    }

    public void OnLose()
    {
        UIManager.Instance.OpenPanel(UIPanel.LoseUI);
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        joystick.SetActive(false);
    }

    public void ContinueGame()
    {
        UIManager.Instance.CloseAllPanel();
        Time.timeScale = 1f;
        joystick.SetActive(true);
    }

    public void Restart()
    {
        SimplePool.Collect(PoolType.DropBrick);
        ContinueGame();
        LevelManager.Instance.OnInit();
    }
}
