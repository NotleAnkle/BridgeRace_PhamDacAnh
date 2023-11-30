using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollier : MonoBehaviour
{
    [SerializeField] private Player player;

    private IEnumerator StandUp()
    {
        yield return new WaitForSeconds(2f);
        player.StandUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = Cache<Bot>.GetScript(other);

            if (bot.playerBrickCount < player.playerBrickCount)
            {
                bot.ChangeState(new FallState());
            }

            if (bot.playerBrickCount > player.playerBrickCount)
            {
                player.Fall();
                StartCoroutine(StandUp());
            }
        }
    }
}
