using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {


	private IEnumerator Start ()
    {
		yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
	}
	
}
