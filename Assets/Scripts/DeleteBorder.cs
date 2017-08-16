using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

public class DeleteBorder : MonoBehaviour
{

    private LevelGenerator _levelGenerator;

	private void Start()
	{
	    _levelGenerator = FindObjectOfType<LevelGenerator>();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.BLOCK_GROUP)
        {
            Destroy(col.gameObject);
            _levelGenerator.GenerateNextBlockGroup();
        }
        else if (col.tag == Tags.COIN || col.tag == Tags.RHUM)
        {
            Destroy(col.gameObject);
        }
    }
}
