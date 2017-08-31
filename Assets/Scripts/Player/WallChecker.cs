using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class WallChecker : MonoBehaviour
    {

        public float TimeForBeatWall;

        private PlayerController _playerController;

        private float _timer;
        private bool _timerIsOn;

        private int _wallsInCollider;

        private void Start()
        {
            _playerController = transform.GetComponentInParent<PlayerController>();
            _wallsInCollider = 0;
        }

        private void Update()
        {
            Timer();
        }

    

        private void OnTriggerEnter2D(Collider2D col)
        {
            if ((col.tag == Tags.FLOOR || col.tag == Tags.BOX /*|| col.tag == Tags.SPIKE*/))
            {
                _wallsInCollider++;
                _playerController.SetCanMove(false);
                if (!_playerController.IsAlive) return;
                _timer = 0f;
                _timerIsOn = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (_playerController.IsAlive && (col.tag == Tags.FLOOR || col.tag == Tags.BOX /*|| col.tag == Tags.SPIKE*/))
            {
                _wallsInCollider--;
                if (_wallsInCollider <= 0)
                {
                    _playerController.SetCanMove(true);
                }
                _timerIsOn = false;
            }
        }

        private void Timer()
        {
            if (!_playerController.IsAlive || !_timerIsOn) return;

            _timer += Time.deltaTime;
            if (_timer >= TimeForBeatWall && !_playerController.IsInvulnerable)
            {
                _timerIsOn = false;
                _playerController.DieBeatWall();
            }
        }

    }
}
