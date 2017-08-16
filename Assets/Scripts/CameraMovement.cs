using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform Target;
    public float LeftDistanceOffset = 3;

	private void Start()
    {
		
	}
	

	private void Update()
	{
	    transform.position = new Vector3(Target.position.x + LeftDistanceOffset, transform.position.y, transform.position.z);
	}
}
