using System;
using System.Collections;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        public float Speed = 2;
        public float JumpForce = 2;
        public float Acceleration = 1.01f;

        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                if (!value)
                {
                    SetPlayerIsNotDrunked();
                    _gameController.PlayerIsDead();
                }
            }
        }

        private bool _isAlive;
        private bool _dieFallingAnimIsStarted;
        public bool IsOnGround
        {
            get { return _anim.GetBool(AnimParameters.Player.OnGround); }
            set { _anim.SetBool(AnimParameters.Player.OnGround, value); }
        }

        private bool _canJump;
        private float _speedUpValue;
        private bool _canMove;
        //private bool _canStartRun;

        private Rigidbody2D _rigidbody;
        private Animator _anim;
        private Animator _drunkMaskAnim;
        private GameController _gameController;
        private CanvasController _canvasController;

        private bool _isDrunk;

        private float _drunkTime;
        private float _drunkTimer;
        
        private bool _haveParrot;

        public float InvulnerabilityTime;
        public bool IsInvulnerable;
        private float _invulnerabilityTimer;

        private static float POS_Y_TO_DISABLE_FALLING = -50;

        private void Start ()
        {
            _gameController = FindObjectOfType<GameController>();
            _canvasController = FindObjectOfType<CanvasController>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _drunkMaskAnim = transform.Find("DrunkMask").GetComponent<Animator>();
            _canMove = false;
            //_canStartRun = true;
            IsAlive = true;
            _speedUpValue = Acceleration * Speed;
        }

        public void StartRun()
        {
            _canMove = true;
            _canJump = true;
            _anim.SetTrigger(AnimParameters.Player.StartRun);
            Jump();
        }


        private void Update()
        {
            CheckPositionToDisableEndlesslyFalling();
            //CheckInputForStartRun();
            PlayerMovement();
            CheckVelocity();
            InvulnerabilityTimer();
        }


        private void CheckPositionToDisableEndlesslyFalling()
        {
            if (transform.position.y < POS_Y_TO_DISABLE_FALLING)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.isKinematic = true;
            }
        }

        //private void CheckInputForStartRun()
        //{
        //    if (!_canStartRun) return;
        //    if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        //    {
        //        _canStartRun = false;
        //        StartRun();
        //    }
        //}

        private void PlayerMovement()
        {
            //JumpCheck();

            if (!_canMove) return;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            SpeedIncreasing();
            DrunkTimer();
        }


        private void SpeedIncreasing()
        {
            Speed = Speed + _speedUpValue;
        }

        private void JumpCheck()
        {
            if (_canJump && IsAlive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                Jump();
            }
        }

        public void ClickedToJump()
        {
            if (_canJump && IsAlive)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            _anim.SetTrigger(AnimParameters.Player.Jump);
            _canJump = false;
        }


        public void SetJumpAbility(bool canJump)
        {
            _canJump = canJump;
        }

        private void CheckVelocity()
        {
            var velocity = _rigidbody.velocity;
            _anim.SetFloat(AnimParameters.Player.VerticalVelocity, velocity.y);
        }

        public void DieJump(JumpAfterTrap jumpAfterTrap)
        {
            if (CheckParrotHaving(jumpAfterTrap)) return;

            _canMove = false;
            IsAlive = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            _anim.SetTrigger(AnimParameters.Player.DieJump);
        }

        /// <summary>
        /// Returns true if player have parrot at this moment
        /// </summary>
        /// <returns></returns>
        private bool CheckParrotHaving(JumpAfterTrap jumpAfterTrap)
        {
            if (!_haveParrot) return false;
            LoseParrot(jumpAfterTrap);
            return true;
        }

        public void FallAfterDieJump()
        {
            _dieFallingAnimIsStarted = true;
            _rigidbody.isKinematic = false;
            _canMove = true;
        }

        public void Falled()
        {
            if (!_dieFallingAnimIsStarted) return;

            _anim.SetBool(AnimParameters.Player.FalledAfterDie, true);

            StartCoroutine(StopMovingOnGround(true));
        }

        private IEnumerator StopMovingOnGround(bool forwardMoving)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            if (forwardMoving)
            {
                if (Speed < 1)
                {
                    _canMove = false;
                }
                else
                {
                    Speed -= 0.3f;
                    StartCoroutine(StopMovingOnGround(true));
                }
            }
            else
            {
                if (Speed > -1)
                {
                    _canMove = false;
                }
                else
                {
                    Speed += 0.3f;
                    StartCoroutine(StopMovingOnGround(false));
                }
            }
        }

        public void DieBeatWall()
        {
            if (CheckParrotHaving(JumpAfterTrap.JumpIfOnTheGround)) return;

            _canMove = false;
            IsAlive = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            _anim.SetTrigger(AnimParameters.Player.DieBeatWall);
        }

        public void FallAfterDieByBeatWall()
        {
            _dieFallingAnimIsStarted = true;
            _rigidbody.isKinematic = false;
            Speed = Speed * -0.5f;
            _canMove = true;
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
        }

        public void DieBurn(JumpAfterTrap jumpAfterTrap)
        {
            if (CheckParrotHaving(jumpAfterTrap)) return;

            _canMove = false;
            IsAlive = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            _anim.SetTrigger(AnimParameters.Player.DieBurn);
        }

        public void GetCoin()
        {
            _gameController.GetCoin();
        }

        public void DiePitFall()
        {
            IsAlive = false;
            StartCoroutine(StopMovingOnGround(Speed > 0));
        }

        public void GetRhum(float drunkTime)
        {
            if (_haveParrot) return;
            SetPlayerDrunked(drunkTime);
        }

        private void SetPlayerDrunked(float drunkTime)
        {
            _anim.SetBool(AnimParameters.Player.IsDrunk, true);
            _drunkMaskAnim.SetBool(AnimParameters.Player.IsDrunk, true);
            _canvasController.SetDrunkseeCanvasActive(true);
            _drunkTimer = 0f;
            _drunkTime = drunkTime;
            _isDrunk = true;
        }

        public void SetPlayerIsNotDrunked()
        {
            _anim.SetBool(AnimParameters.Player.IsDrunk, false);
            _drunkMaskAnim.SetBool(AnimParameters.Player.IsDrunk, false);
            _canvasController.SetDrunkseeCanvasActive(false);
            _isDrunk = false;
        }
    

        private void DrunkTimer()
        {
            if (!_isDrunk) return;

            _drunkTimer += Time.deltaTime;
            if (_drunkTimer >= _drunkTime)
            {
                SetPlayerIsNotDrunked();
            }
        }

        public void GetParrot()
        {
            _anim.SetTrigger(AnimParameters.Player.GetParrot);
            _haveParrot = true;
        }

        public void LoseParrot(JumpAfterTrap jumpAfterTrap)
        {
            _anim.SetTrigger(AnimParameters.Player.LoseParrot);
            _haveParrot = false;
            StartInvulnerabilityTimer();
            if (jumpAfterTrap == JumpAfterTrap.NoJump)
            {
            }
            else if (jumpAfterTrap == JumpAfterTrap.JumpIfOnTheGround)
            {
                ClickedToJump();
            }
            else if (jumpAfterTrap == JumpAfterTrap.JumpAnyone)
            {
                Jump();
            }
        }

        private void StartInvulnerabilityTimer()
        {
            _invulnerabilityTimer = 0f;
            IsInvulnerable = true;
        }

        private void InvulnerabilityTimer()
        {
            if (!IsInvulnerable) return;

            _invulnerabilityTimer += Time.deltaTime;
            if (_invulnerabilityTimer >= InvulnerabilityTime)
            {
                IsInvulnerable = false;
            }
        }

        public void TakeScore(int scoreAmount)
        {
            _gameController.AddRunScore(scoreAmount);
        }

    }
}
