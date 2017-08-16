using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class Spike : Trap
    {
        
        protected override void PlayerCatched(Collider2D playerCollider)
        {
            var player = playerCollider.gameObject.GetComponent<PlayerController>();
            if (!player.IsAlive || player.IsInvulnerable) return;
            //col.transform.position = transform.position + new Vector3(0, 1, 0);
            player.DieJump(PlayerAutoJumpAfterCatch);
        }
    }
}
