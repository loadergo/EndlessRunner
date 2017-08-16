using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.PLAYER)
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            player.GetCoin();
            Destroy(gameObject);
        }
    }

}
