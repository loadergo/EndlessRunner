using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Background;
using UnityEngine;

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
