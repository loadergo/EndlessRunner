using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraMovement : MonoBehaviour
{

    public Transform Target;
    public float LeftDistanceOffset = 3;

    public float BlurSpeedUp;
    private MotionBlur _motionBlur;
    private float _blurValue;
    private float _blurTargetValue;
    private bool _changeBlur;



	private void Start()
	{
	    _motionBlur = GetComponent<MotionBlur>();
	    _motionBlur.enabled = false;
	    _blurValue = _motionBlur.blurAmount;
	    _motionBlur.blurAmount = 0;
	}
	

	private void Update()
	{
	    transform.position = new Vector3(Target.position.x + LeftDistanceOffset, transform.position.y, transform.position.z);

	    if (_changeBlur)
	    {
	        if (_blurTargetValue <= 0)
	        {
	            _motionBlur.blurAmount -= BlurSpeedUp * Time.deltaTime;
	            if (_motionBlur.blurAmount <= 0)
	            {
	                _changeBlur = false;
	                _motionBlur.enabled = false;
	            }
	        }
	        else
	        {
                _motionBlur.blurAmount += BlurSpeedUp * Time.deltaTime;
                if (_motionBlur.blurAmount >= _blurValue)
                {
                    _changeBlur = false;
                }
            }
	    }
	}

    public void SetOnBlur(bool isOn)
    {
        if (isOn)
        {
            _motionBlur.enabled = true;
            _blurTargetValue = _blurValue;

        }
        else
        {
            _blurTargetValue = 0f;
        }

        _changeBlur = true;
    }
}
