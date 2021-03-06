﻿using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class Fire : Trap
    {

        protected override void PlayerCatched(Collider2D playerCollider)
        {
            var player = playerCollider.gameObject.GetComponent<PlayerController>();
            if (!player.IsAlive || player.IsInvulnerable) return;
            player.DieBurn(PlayerAutoJumpAfterCatch);
        }
    }
}
