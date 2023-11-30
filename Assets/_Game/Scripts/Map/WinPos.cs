using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnWin();
        }

        if (other.CompareTag("Bot"))
        {
            GameManager.Instance.OnLose();
        }
    }
}
