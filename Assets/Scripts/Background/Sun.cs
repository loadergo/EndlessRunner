using UnityEngine;

namespace Assets.Scripts.Background
{
    public class Sun : MonoBehaviour
    {

        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public Vector2 RisePositon;
        public Vector2 FallPosition;
        public float MoveSpeed;
        public float RiseSpeed;
        public float RiseSmoothCoef;
        public float RiseSmoothCoefIncrease;

        public Sprite SunSprite;
        public Sprite MoonSprite;

        private SpriteRenderer _spriteRenderer;
        private GameController _gameController;
        private LevelGenerator _levelGenerator;

        private bool _isRising;
        private bool _isFalling;
        private bool _isNight;

        private float _currRiseSpeed;
        private float _currRiseSmoothCoef;

        private bool _canMove;


        private void Start ()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gameController = FindObjectOfType<GameController>();
            _levelGenerator = FindObjectOfType<LevelGenerator>();
            _currRiseSpeed = RiseSpeed;
            _currRiseSmoothCoef = RiseSmoothCoef;
            _isNight = false;
        }
	

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (!_canMove) return;

            HorizontaMovement();
            if (_isRising)
            {
                Rising();
            }
            else if (_isFalling)
            {
                Falling();
            }
            else
            {
                CheckFalling();
            }
        }

        private void HorizontaMovement()
        {
            transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
        }

        private void CheckFalling()
        {
            if (transform.localPosition.x <= FallPosition.x)
            {
                _currRiseSpeed = RiseSpeed / 100;
                _currRiseSmoothCoef = RiseSmoothCoef;
                _isFalling = true;
            }
        }

        private void Falling()
        {
            _currRiseSmoothCoef += RiseSmoothCoefIncrease;
            _currRiseSpeed += _currRiseSmoothCoef;
            if (_currRiseSpeed > RiseSpeed)
            {
                _currRiseSpeed = RiseSpeed;
            }

            transform.Translate(Vector3.down * Time.deltaTime * _currRiseSpeed);
            if (transform.localPosition.x <= EndPosition.x)
            {
                Change();
            }
        }

        private void Rising()
        {
            _currRiseSmoothCoef += RiseSmoothCoefIncrease;
            _currRiseSpeed -= _currRiseSmoothCoef;
            if (_currRiseSpeed < 0)
            {
                _currRiseSpeed = 0;
            }

            transform.Translate(Vector3.up * Time.deltaTime * _currRiseSpeed);
            if (transform.localPosition.x <= RisePositon.x || transform.localPosition.y >= RisePositon.y)
            {
                _isRising = false;
            }
        }

        private void Change()
        {
            transform.localPosition = StartPosition;
            _isNight = !_isNight;
            _spriteRenderer.sprite = _isNight ? MoonSprite : SunSprite;
            _gameController.ChangeDay(_isNight);

            _currRiseSpeed = RiseSpeed;
            _currRiseSmoothCoef = RiseSmoothCoef;
            _isRising = true;
            _isFalling = false;
        }

        public void CanMove(bool canMove)
        {
            _canMove = canMove;
        }
    }
}
