using UnityEngine;

namespace Assets.Scripts.Background
{
    public class Cloud : SunDependent
    {

        public float MoveSpeed;
        public float ShiftPositionX;
        public float RestartPositionX;


        protected override void Start ()
        {
            base.Start();
        }

        protected override void SetNightSprite()
        {
            NightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    
        protected override void CustomUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            transform.Translate(Vector2.left * Time.deltaTime * MoveSpeed);
            if (transform.localPosition.x <= ShiftPositionX)
            {
                Shift();
            }
        }

        private void Shift()
        {
            transform.localPosition = new Vector2(RestartPositionX, transform.localPosition.y);
        }

    }
}
