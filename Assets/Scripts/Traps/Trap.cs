using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public abstract class Trap : MonoBehaviour
    {
        public JumpAfterTrap PlayerAutoJumpAfterCatch = JumpAfterTrap.NoJump;
        public bool IsScoreTrap = true;
        public int Score = 1;

        private bool _passed = false;

        protected PlayerController Player;
        
        protected abstract void PlayerCatched(Collider2D playerCollider);

        protected virtual void Start()
        {
            Player = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == Tags.PLAYER)
            {
                PlayerCatched(col);
            }
        }

        private void Update()
        {
            CheckPassing();
        }

        private void CheckPassing()
        {
            if (!IsScoreTrap || _passed) return;

            if (Player.transform.position.x > transform.position.x)
            {
                Player.TakeScore(Score);
                _passed = true;
            }
        }
    }
}
