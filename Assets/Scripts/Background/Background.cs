using UnityEngine;

namespace Assets.Scripts.Background
{
    public class Background : SunDependent
    {
        protected override void SetNightSprite()
        {
            NightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        protected override void CustomUpdate()
        {
        }
    }
}