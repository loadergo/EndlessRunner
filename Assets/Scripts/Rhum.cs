using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

public class Rhum : MonoBehaviour
{

    public float DrunkTime = 10f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.PLAYER)
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            if (player.IsAlive)
            {
                player.GetRhum(DrunkTime);
            }
            Destroy(gameObject);
        }
    }
}
