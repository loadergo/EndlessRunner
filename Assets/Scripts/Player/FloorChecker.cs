using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{

    private PlayerController _playerController;
    
    private int _floorCollidersCount;

    private void Start ()
	{
	    _playerController = transform.GetComponentInParent<PlayerController>();
	    _floorCollidersCount = 0;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.FLOOR)
        {
            if (!_playerController.IsAlive)
            {
                _playerController.Falled();
            }

            _floorCollidersCount++;
            _playerController.SetJumpAbility(true);
            _playerController.IsOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == Tags.FLOOR)
        {
            _floorCollidersCount--;
            if (_floorCollidersCount == 0)
            {
                _playerController.SetJumpAbility(false);
                _playerController.IsOnGround = false;
            }
        }
    }
}
