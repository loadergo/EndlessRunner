using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

public class BlackScreen : MonoBehaviour {

    private Animator _anim;
    private Action _closeScreenCallback;

    private void Awake ()
    {
        _anim = GetComponent<Animator>();
        _closeScreenCallback = () => { };
    }


    public void OpenScreen()
    {

        _anim.SetTrigger(AnimParameters.BlackScreen.Open);
    }

    public void CloseScreen(Action callback)
    {
        _anim.SetTrigger(AnimParameters.BlackScreen.Close);
        _closeScreenCallback = callback;
    }

    public void InvokeCallBack()
    {
        _closeScreenCallback.Invoke();
    }
    
}
