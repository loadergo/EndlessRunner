using UnityEngine;

namespace Assets.Scripts.Background
{
    public abstract class SunDependent : MonoBehaviour {
    
        public static float ChangeNightColorSpeed = 0.3f;

        protected SpriteRenderer NightSprite;

        private bool _setDaySprite;
        private bool _setNightSprite;
        private bool _isWorking;

        protected virtual void Start ()
        {
            SetNightSprite();
        }

        protected abstract void SetNightSprite();

        // Update is called once per frame
        private void Update ()
        {
            if (!_isWorking) return;
            ChangingNightSprite();
            CustomUpdate();
        }

        protected abstract void CustomUpdate();

        public void ChangeDay(bool isNight)
        {
            _setDaySprite = !isNight;
            _setNightSprite = isNight;
        }

        private void ChangingNightSprite()
        {
            if (_setDaySprite)
            {
                NightSprite.color = new Color(1, 1, 1, NightSprite.color.a - Time.deltaTime * ChangeNightColorSpeed);
                if (NightSprite.color.a <= 0)
                {
                    _setDaySprite = false;
                }
                //_nightSprite.color = Color.Lerp(Color.white, Color.clear, ChangeNightColorSpeed * Time.deltaTime);
            }
            else if (_setNightSprite)
            {
                NightSprite.color = new Color(1, 1, 1, NightSprite.color.a + Time.deltaTime * ChangeNightColorSpeed);
                if (NightSprite.color.a >= 1)
                {
                    _setNightSprite = false;
                }
                //_nightSprite.color = Color.LerpUnclamped(Color.clear, Color.white, ChangeNightColorSpeed * Time.deltaTime);
            }
        }

        public void Move()
        {
            _isWorking = true;
        }
        public void Stop()
        {
            _isWorking = false;
        }
    }
}
