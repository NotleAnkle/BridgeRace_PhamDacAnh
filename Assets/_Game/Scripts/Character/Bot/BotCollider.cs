using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollider : MonoBehaviour
{
    [SerializeField] Bot bot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Bot bot = other.GetComponent<Bot>();

            if (bot.playerBrickCount < this.bot.playerBrickCount)
            {
                bot.ChangeState(new FallState());
            }

            if (bot.playerBrickCount > this.bot.playerBrickCount)
            {
               this.bot.ChangeState(new FallState());
            }
        }
    }
}
