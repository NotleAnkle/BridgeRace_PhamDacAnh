using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            GameManager.Instance.OnWin();
        }

        if (other.CompareTag(Constant.TAG_BOT))
        {
            GameManager.Instance.OnLose();
        }
    }
}
